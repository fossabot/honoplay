import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import {
    DialogContent,
    Grid,
    CircularProgress,
    MuiThemeProvider,
    Divider
} from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Chip from '../../components/Chip/ChipComponent';

import { connect } from "react-redux";
import {
    fetchWorkingStatusList,
    postWorkingStatus
} from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus";

class WorkingStatus extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            loadingCreate: false,
            createError: false,
            workingStatuses: [],
        }
    }

    workingStatusModel = {
        name: ''
    };

    componentDidUpdate(prevProps) {
        const {
            isWorkingStatusCreateLoading,
            newWorkingStatus,
            errorWorkingStatusCreate,
            isWorkingStatusListLoading,
            workingStatusList,
            errorWorkingStatusList
        } = this.props;

        if (!prevProps.isWorkingStatusListLoading && isWorkingStatusListLoading && workingStatusList) {
            if (!errorWorkingStatusList) {
                this.setState({
                    workingStatuses: workingStatusList.items
                });
            }
        }

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
        if (prevProps.isWorkingStatusCreateLoading && !isWorkingStatusCreateLoading && newWorkingStatus) {
            this.props.fetchWorkingStatusList(0, 50);
            if (!errorWorkingStatusCreate) {
                this.setState({
                    loadingCreate: false,
                    createError: false
                });
            }
        }
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.workingStatusModel[name] = value;
        this.setState({
            createError: false
        })
    }

    handleClick = () => {
        this.props.postWorkingStatus(this.workingStatusModel);
    }

    componentDidMount() {
        this.props.fetchWorkingStatusList(0, 50);
    }

    render() {
        const { classes } = this.props;
        const {
            loadingCreate,
            createError,
            workingStatuses
        } = this.state;

        return (

            <MuiThemeProvider theme={theme}>
                <div className={classes.root}>
                    <DialogContent>
                        <Grid container spacing={24}>
                            <Grid item xs={10} sm={11}>
                                <Input
                                    error={createError}
                                    onChange={this.handleChange}
                                    labelName={translate('WorkingStatus')}
                                    inputType="text"
                                    name="name"
                                    value={this.workingStatusModel.name}
                                />
                            </Grid>
                            <Grid item xs={2} sm={1}>
                                <Button
                                    buttonColor="secondary"
                                    buttonName={translate('Add')}
                                    onClick={this.handleClick}
                                    disabled={loadingCreate}
                                />
                                {loadingCreate && (
                                    <CircularProgress
                                        size={24}
                                        disableShrink={true}
                                        className={classes.buttonProgress}
                                    />
                                )}
                            </Grid>
                            <Grid item xs={12} sm={12}>
                                <Divider />
                            </Grid>
                            <Grid item xs={12} sm={12}>
                                <Chip
                                    data={workingStatuses}>
                                </Chip>
                            </Grid>
                        </Grid>
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

    let newWorkingStatus = workingStatusCreate;

    const {
        isWorkingStatusListLoading,
        workingStatusList,
        errorWorkingStatusList
    } = state.workingStatusList;


    return {
        isWorkingStatusCreateLoading,
        newWorkingStatus,
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
