import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from "classnames";
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { fetchTrainersList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer";
import { fetchClassroom, updateClassroom, fetchClassroomListByTrainingId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom';

class ClassroomUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trainer: null,
            classroom: {
                name: '',
                trainerUserId: '',
                trainingId: '',
            },
            loadingClassroom: false,
            updateError: false,
            loadingUpdate: false,
            success: false
        };
    }

    classroomId = null;
    trainingId = null;

    componentDidUpdate(prevProps) {
        const {
            isTrainerListLoading,
            errorTrainerList,
            trainersList,
            isClassroomLoading,
            classroom,
            errorClassroom,
            isUpdateClassroomLoading,
            updateClassroom,
            errorUpdateClassroom
        } = this.props;

        if (prevProps.isTrainerListLoading && !isTrainerListLoading && trainersList) {
            if (!errorTrainerList) {
                this.setState({
                    trainer: trainersList.items,
                });
            }
        }
        if (prevProps.isClassroomLoading && !isClassroomLoading) {
            this.setState({
                loadingClassroom: true
            })
        }
        if (prevProps.isClassroomLoading && !isClassroomLoading && classroom) {
            if (!errorClassroom) {
                this.setState({
                    classroom: classroom.items[0]
                })
            }
        }

        if (!prevProps.isUpdateClassroomLoading && isUpdateClassroomLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateClassroom && errorUpdateClassroom) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateClassroomLoading && !isUpdateClassroomLoading && updateClassroom) {
            if (!errorUpdateClassroom) {
                this.props.fetchClassroomListByTrainingId(this.trainingId);
                this.setState({
                    updateError: false,
                    loadingUpdate: false,
                    success: true,
                });
                setTimeout(() => {
                    this.setState({ success: false });
                }, 1000);
            }
        }
    }

    componentDidMount() {
        this.props.fetchTrainersList(0,50);
        this.props.fetchClassroom(this.classroomId);
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            classroom: {
                ...prevState.classroom,
                [name]: value
            },
            updateError: false
        }))
    }

    handleClick = () => {
        this.props.updateClassroom(this.state.classroom);
    }

    render() {
        const { trainer, classroom, loadingClassroom, loadingUpdate, success } = this.state;
        const { classes, classroomId, trainingId } = this.props;

        this.classroomId = classroomId;
        this.trainingId = trainingId;

        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });

        return (

            <div className={classes.root}>
                {loadingClassroom == false ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={24}>
                        <Grid item xs={12} sm={12} >
                            <Input
                                labelName={translate('ClassroomName')}
                                inputType="text"
                                value={classroom.name}
                                name="name"
                                onChange={this.handleChange}
                            />
                            <DropDown
                                labelName={translate('Trainer')}
                                data={trainer}
                                value={classroom.trainerUserId}
                                name="trainerUserId"
                                onChange={this.handleChange}
                            />
                        </Grid>
                        <Grid item xs={12} sm={11} />
                        <Grid item xs={12} sm={1}>
                            <Button
                                buttonColor="primary"
                                buttonName={translate('Update')}
                                onClick={this.handleClick}
                                disabled={loadingUpdate}
                                className={buttonClassname}
                            />
                            {loadingUpdate && (
                                <CircularProgress
                                    size={24}
                                    disableShrink={true}
                                    className={classes.buttonProgressUpdate}
                                />
                            )}
                        </Grid>
                    </Grid>
                }
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isTrainerListLoading,
        errorTrainerList,
        trainersList
    } = state.trainersList;

    const {
        isClassroomLoading,
        classroom,
        errorClassroom
    } = state.classroom;

    const {
        isUpdateClassroomLoading,
        updateClassroom,
        errorUpdateClassroom
    } = state.updateClassroom;

    return {
        isTrainerListLoading,
        errorTrainerList,
        trainersList,
        isClassroomLoading,
        classroom,
        errorClassroom,
        isUpdateClassroomLoading,
        updateClassroom,
        errorUpdateClassroom,
    };
};

const mapDispatchToProps = {
    fetchClassroom,
    updateClassroom,
    fetchClassroomListByTrainingId,
    fetchTrainersList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(ClassroomUpdate));