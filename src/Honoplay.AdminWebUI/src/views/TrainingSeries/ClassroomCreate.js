import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { fetchTrainersList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer";
import { createClassroom, fetchClassroomList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom";

class ClassroomCreate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            classroomLoading: false,
            classroomError: false,
            trainer: null,
            classroom: {
                createClassroomModels: [
                    {
                        name: '',
                        trainerId: '',
                        trainingId: '',
                    }
                ]
            }
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isTrainerListLoading,
            errorTrainerList,
            trainersList,
            isCreateClassroomLoading,
            createClassroom,
            errorCreateClassroom
        } = this.props;

        if (prevProps.isTrainerListLoading && !isTrainerListLoading && trainersList) {
            if (!errorTrainerList) {
                this.setState({
                    trainer: trainersList.items,
                });
            }
        }

        if (!prevProps.isCreateClassroomLoading && isCreateClassroomLoading) {
            this.setState({
                classroomLoading: true
            })
        }
        if (!prevProps.errorCreateClassroom && errorCreateClassroom) {
            this.setState({
                classroomError: true,
                classroomLoading: false
            })
        }
        if (prevProps.isCreateClassroomLoading && !isCreateClassroomLoading && createClassroom) {
            this.props.fetchClassroomList(0, 50);
            if (!errorCreateClassroom) {
                this.setState({
                    classroomLoading: false,
                    classroomError: false,
                });
            }
        }
    }

    componentDidMount() {
        this.props.fetchTrainersList(0, 50);
    }

    handleClick = () => {
        this.props.createClassroom(this.state.classroom);
    }

    render() {
        const { classroomLoading, trainer, classroom, classroomError } = this.state;
        const { classes, trainingId } = this.props;

        this.state.classroom.createClassroomModels.map((classroom) => {
            classroom.trainingId = trainingId;
        })

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    {classroom.createClassroomModels.map((classroom, id) => (
                        <Grid item xs={12} sm={12} key={id}>
                            <Input
                                error={classroomError}
                                labelName={translate('ClassroomName')}
                                inputType="text"
                                onChange={e => {
                                    classroom.name = e.target.value;
                                    this.setState({ classroomError: false });
                                }}
                            />
                            <DropDown
                                error={classroomError}
                                data={trainer}
                                labelName={translate('Trainer')}
                                onChange={e => {
                                    classroom.trainerId = e.target.value;
                                    this.setState({ classroomError: false });
                                }}
                            />
                        </Grid>
                    ))}
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            onClick={this.handleClick}
                            disabled={classroomLoading}
                        />
                        {classroomLoading && (
                            <CircularProgress
                                size={24}
                                disableShrink={true}
                                className={classes.buttonProgress}
                            />
                        )}
                    </Grid>
                </Grid>
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
        isCreateClassroomLoading,
        createClassroom,
        errorCreateClassroom
    } = state.createClassroom;

    return {
        isTrainerListLoading,
        errorTrainerList,
        trainersList,
        isCreateClassroomLoading,
        createClassroom,
        errorCreateClassroom
    };
};

const mapDispatchToProps = {
    fetchTrainersList,
    createClassroom,
    fetchClassroomList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(ClassroomCreate));