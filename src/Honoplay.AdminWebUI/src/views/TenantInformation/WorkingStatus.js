import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import {
    DialogContent,
    List,
    FormGroup,
    FormControlLabel,
    Radio,
    Grid,
    CircularProgress,
    MuiThemeProvider,
} from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import {
    fetchWorkingStatusList,
    postWorkingStatus
} from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus";

class WorkingStatus extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            selectedValue: '',
            loadingCreate: false,
            loadingList: false,
            workingStatusModel: {
                name: ''
            },
            createError: false,
            listError: false,
            workingStatus: [],
            checked: false
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isWorkingStatusCreateLoading,
            workingStatusCreate,
            errorWorkingStatusCreate,
            isWorkingStatusListLoading,
            workingStatusList,
            errorWorkingStatusList
        } = this.props;

        if (!prevProps.isWorkingStatusCreateLoading && isWorkingStatusCreateLoading) {
            this.setState({
                loadingCreate: true
            })
        }
        if (!prevProps.errorWorkingStatusCreate && errorWorkingStatusCreate) {
            this.setState({
                createError: true,
                loadingCreate: false
            })
        }
        if (prevProps.isWorkingStatusCreateLoading && !isWorkingStatusCreateLoading && workingStatusCreate) {
            if (!errorWorkingStatusCreate) {
                this.setState({
                    loadingCreate: false,
                    createError: false
                });
            }
        }
        if (prevProps.isWorkingStatusListLoading && !isWorkingStatusListLoading) {
            this.setState({
                loadingList: true
            })
        }
        if (!prevProps.errorWorkingStatusList && errorWorkingStatusList) {
            this.setState({
                listError: true,
                loadingList: false
            })
        }
        if (prevProps.isWorkingStatusListLoading && !isWorkingStatusListLoading && workingStatusList) {
            this.props.fetchWorkingStatusList(0, 50);
            if (!errorWorkingStatusList) {
                this.setState({
                    listError: true,
                    loadingList: false
                });
            }
        }
    }

    handleChange = (e) => {
        this.setState({
            workingStatusModel: {
                name: e.target.value
            },
            createError: false
        })
    }

    handleChangeValue = (event) => {
        this.setState({
            selectedValue: event.target.value,
        });
    };

    handleClick = () => {
        const { postWorkingStatus, fetchWorkingStatusList } = this.props;
        const { workingStatusModel } = this.state;
        postWorkingStatus(workingStatusModel);
        fetchWorkingStatusList(0, 50);
        this.setState({
            workingStatusModel: {
                name: ''
            },
        })
    }

    render() {
        const { classes, data } = this.props;
        const {
            loading,
            loadingCreate,
            workingStatusModel,
            createError,
            loadingList,
            selectedValue,
        } = this.state;
        return (

            <MuiThemeProvider theme={theme}>
                <div className={classes.root}>
                    <DialogContent>
                        {loadingList ?
                            <CircularProgress
                                size={50}
                                disableShrink={true}
                                className={classes.loadingProgress}
                            /> :
                            <Grid container spacing={24}>
                                <Grid item xs={10} sm={11}>
                                    <Input
                                        error={createError}
                                        onChange={this.handleChange}
                                        labelName={translate('WorkingStatus')}
                                        inputType="text"
                                        name="name"
                                        value={workingStatusModel.name}
                                    />
                                </Grid>
                                <Grid item xs={2} sm={1}>
                                    <Button
                                        buttonColor="secondary"
                                        buttonName={translate('Save')}
                                        onClick={this.handleClick}
                                        disabled={loading}
                                    />
                                    {loadingCreate && (
                                        <CircularProgress
                                            size={24}
                                            disableShrink={true}
                                            className={classes.addProgress}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={12} sm={12}></Grid>
                                <Grid item xs={12} sm={12}>
                                    <div
                                        className={classes.workingStatusDiv}>
                                        {data.map((data, id) => {
                                            return (
                                                <List
                                                    dense
                                                    key={id}
                                                    className={classes.workingStatusList}>
                                                    <FormGroup row>
                                                        <FormControlLabel
                                                            control={
                                                                <Radio checked={selectedValue === data.name}
                                                                    onClick={this.handleChangeValue}
                                                                    value={data.name}
                                                                    color='secondary'
                                                                />
                                                            }
                                                            label={data.name}
                                                        />
                                                    </FormGroup>
                                                </List>
                                            );
                                        })}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={12}></Grid>
                                <Grid item xs={12} sm={11}></Grid>
                                <Grid item xs={12} sm={1}>
                                    <Button
                                        buttonColor="primary"
                                        buttonName={translate('Save')}
                                    />
                                </Grid>
                            </Grid>
                        }
                    </DialogContent>
                </div>
            </MuiThemeProvider>
        );
    }
}


const mapStateToProps = state => {

    const {
        isWorkingStatusCreateLoading,
        workingStatusCreate,
        errorWorkingStatusCreate
    } = state.workingStatusCreate;
    const {
        isWorkingStatusListLoading,
        workingStatusList,
        errorWorkingStatusList
    } = state.workingStatusList;

    return {
        isWorkingStatusCreateLoading,
        workingStatusCreate,
        errorWorkingStatusCreate,
        isWorkingStatusListLoading,
        workingStatusList,
        errorWorkingStatusList
    };
};

const mapDispatchToProps = {
    postWorkingStatus,
    fetchWorkingStatusList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(WorkingStatus));
