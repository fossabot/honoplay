import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Checkbox, MuiThemeProvider, Divider, Typography } from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { createOption } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Options";

class Options extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            optionError: false,
            options: [
                {
                    questionId: '',
                    text: '',
                    visibilityOrder: '',
                    isCorrect: false
                }
            ],
            optionsColumns: [
                { title: "Seçenekler", field: "text" },
                { title: "Sıra", field: "visibilityOrder" },
              ],
        }
    }

    optionsModel = {
        createOptionModels: [

        ]
    }

    componentDidUpdate(prevProps) {
        const {
            isCreateOptionLoading,
            createOption,
            errorCreateOption
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
            if (!errorCreateOption) {
                this.setState({
                    loading: false,
                    optionError: false,
                });
            }
        }

    }

    handleClick = () => {
        this.optionsModel.createOptionModels = this.state.options;
        const { createOption } = this.props;
        createOption(this.optionsModel);
    }

    handleChangeChecked = (e) => {
        this.state.options.map((option, id) => {
            option.isCorrect = true;
        })
    }

    optionAdd = () => {
        this.setState({
            options: this.state.options.concat([{
                questionId: '',
                text: '',
                visibilityOrder: '',
                isCorrect: false
            }])
        });
    }


    optionRemove = id => () => {
        this.setState({
            options: this.state.options.filter((o, oid) => id !== oid)
        });
    };

    render() {
        const { classes, key, questionId, loading } = this.props;

        this.state.options.map((option, id) => {
            option.questionId = questionId;
        })

        console.log(this.state.options);
        return (

            <MuiThemeProvider theme={theme} >
                <div className={classes.root}>
                    <Grid container spacing={16}>
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={12}>
                            <Typography>
                                Doğru Cevap
                            </Typography>
                        </Grid>
                        {this.state.options.map((option, id) => (
                            <Grid container direction="row" spacing={16} key={id}>
                                <Grid item xs={12} sm={1}>
                                    <Checkbox
                                        checked={this.state.options.isCorrect}
                                        color='secondary'
                                        value={this.state.text}
                                        onChange={this.handleChangeChecked}
                                        className={classes.checkbox}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={1}>
                                    <Input
                                        inputType="text"
                                        placeholder={translate('VisibilityOrder')}
                                        onChange={e => {
                                            option.visibilityOrder = e.target.value;
                                        }}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={2} >
                                    <Input
                                        inputType="text"
                                        placeholder={translate('Option')}
                                        onChange={e => {
                                            option.text = e.target.value
                                        }}
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
                        <Grid item xs={12} sm={11} ></Grid>
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
                        </Grid>
                        <Grid item xs={12} sm={12}>

                        </Grid>
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

    return {
        isCreateOptionLoading,
        createOption,
        errorCreateOption
    };
};

const mapDispatchToProps = {
    createOption
};


export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Options));