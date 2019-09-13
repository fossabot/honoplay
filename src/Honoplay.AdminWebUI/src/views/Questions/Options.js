import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Checkbox, MuiThemeProvider, Divider, Typography, CircularProgress } from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { createOption, fetchOptionsByQuestionId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Options";

class Options extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            optionError: false,
            order: 1,
            options: [
                {
                    questionId: this.props.questionId,
                    text: '',
                    visibilityOrder: 1,
                    isCorrect: false
                }
            ],
            //questionId: props.questionId
        }
    }

    // questionId = null;

    optionsModel = {
        createOptionModels: [

        ]
    }

    componentDidUpdate(prevProps) {
        const {
            isCreateOptionLoading,
            createOption,
            errorCreateOption,
            isOptionListByQuestionIdLoading,
            optionsListByQuestionId,
        } = this.props;

        if (!prevProps.isCreateOptionLoading && isCreateOptionLoading) {
            this.setState({
                loading: true
            })
        }
        if (!prevProps.errorCreateOption && errorCreateOption) {
            this.setState({
                optionError: true,
                loading: false
            })
        }
        if (prevProps.isCreateOptionLoading && !isCreateOptionLoading && createOption) {
            this.props.fetchOptionsByQuestionId(this.questionId);
            if (!errorCreateOption) {
                this.setState({
                    loading: false,
                    optionError: false,
                });
            }
        }
        if (prevProps.isOptionListByQuestionIdLoading && !isOptionListByQuestionIdLoading && optionsListByQuestionId) {
            this.setState({
                options: optionsListByQuestionId.items
            })
        }

    }

    componentDidMount() {
        this.props.fetchOptionsByQuestionId(this.questionId);
    }

    handleClick = () => {
        this.optionsModel.createOptionModels = this.state.options;
        this.props.createOption(this.optionsModel);
    }

    optionAdd = () => {
        this.setState({
            order: ++this.state.order,
            options: this.state.options.concat([{
                questionId: '',
                text: '',
                visibilityOrder: this.state.order,
                isCorrect: false
            }])
        });
    }

    handleChangeOption = id => evt => {
        const newOptions = this.state.options.map((option, optionId) => {
            if (id !== optionId) return option;
            return { ...option, text: evt.target.value };
        });
        this.setState({ options: newOptions });

        this.optionsModel.createOptionModels = this.state.options;
        this.props.basicOptionModel(this.optionsModel);
    };


    optionRemove = id => () => {
        this.setState({
            order: --this.state.order,
            options: this.state.options.filter((o, oid) => id !== oid)
        });
    };

    render() {
        const { loading, questionId } = this.state;
        const { classes } = this.props;

        // this.questionId = questionId;

        // this.state.options.map((option, id) => {
        //     option.questionId = this.questionId;
        // })

        return (

            <MuiThemeProvider theme={theme} >
                <div className={classes.root}>
                    <Grid container spacing={3}>
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={12}>
                            <Typography>
                                {translate('CorrectAnswer')}
                            </Typography>
                        </Grid>
                        {this.state.options.map((option, id) => (
                            <Grid container direction="row" spacing={3} key={id}>
                                <Grid item xs={12} sm={1}>
                                    <Checkbox
                                        checked={this.state.options.isCorrect}
                                        color='secondary'
                                        value={this.state.options.text}
                                        onChange={e => {
                                            option.isCorrect = true;
                                            this.optionsModel.createOptionModels = this.state.options;
                                            this.props.basicOptionModel(this.optionsModel);
                                        }}
                                        className={classes.checkbox}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={1}>
                                    <Input
                                        inputType="text"
                                        placeholder={translate('VisibilityOrder')}
                                        value={option.visibilityOrder}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={2} >
                                    <Input
                                        inputType="text"
                                        placeholder={translate('Option')}
                                        onChange={this.handleChangeOption(id)}
                                        value={option.text}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={1}>
                                    <Button
                                        buttonColor="secondary"
                                        onClick={this.optionRemove(id)}
                                        buttonIcon="minus"
                                    />
                                </Grid>
                                <Grid item xs={12} sm={7} ></Grid>
                                <Grid item xs={12} sm={12} ></Grid>
                            </Grid>
                        ))}
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={12} >
                            <Button
                                buttonColor="secondary"
                                buttonName={translate('AddOption')}
                                onClick={this.optionAdd}
                                buttonIcon="plus"
                            />
                        </Grid>
                        <Grid item xs={12} sm={12}> <Divider /> </Grid>
                        {/* <Grid item xs={12} sm={11} ></Grid>
                        <Grid item xs={12} sm={1} >
                            <Button
                                buttonColor="primary"
                                buttonName={translate('Save')}
                                onClick={this.handleClick}
                                disabled={loading}
                            />
                            {loading && (
                                <CircularProgress
                                    size={24}
                                    disableShrink={true}
                                    className={classes.buttonProgress}
                                />
                            )}
                        </Grid> */}
                    </Grid>
                </div>
            </MuiThemeProvider>
        );
    }
}

const mapStateToProps = state => {
    const {
        isCreateOptionLoading,
        createOption,
        errorCreateOption
    } = state.createOption;

    const {
        isOptionListByQuestionIdLoading,
        optionsListByQuestionId,
        errorOptionListByQuestionId
    } = state.optionListByQuestionId;

    return {
        isCreateOptionLoading,
        createOption,
        errorCreateOption,
        isOptionListByQuestionIdLoading,
        optionsListByQuestionId,
        errorOptionListByQuestionId
    };
};

const mapDispatchToProps = {
    createOption,
    fetchOptionsByQuestionId
};


export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Options));