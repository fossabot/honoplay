import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import TrainingUpdate from './Training/TrainingUpdate';
import Classrooms from './Classroom/Classrooms';
import Summary from './Summary/Summary';
import Session from './Session/Session';

import { connect } from "react-redux";
import { updateTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";
import { increment, decrement, reset } from '../../redux/actions/ActiveStepActions';
import { changeId } from '../../redux/actions/ClassroomIdActions';

class TrainingSeriesInformation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            newTrainingModel: null,
            loadingUpdate: false,
            updateError: false
        }
    }

    trainingId = localStorage.getItem("trainingId");

    componentDidUpdate(prevProps) {
        const {
            isUpdateTrainingLoading,
            updateTraining,
            errorUpdateTraining
        } = this.props;

        if (!prevProps.isUpdateTrainingLoading && isUpdateTrainingLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateTraining && errorUpdateTraining) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
            })
        }
        if (prevProps.isUpdateTrainingLoading && !isUpdateTrainingLoading && updateTraining) {
            if (!errorUpdateTraining) {
                this.setState({
                    updateError: false,
                    loadingUpdate: false,
                });
                this.props.increment(this.props.activeStep);
            }
        }
    }
    
    componentWillUnmount() {
        this.props.reset();
    }


    handleBack = () => {
        this.props.decrement(this.props.activeStep);
    }

    handleReset = () => {
        this.setState({
            activeStep: 0,
        });
    }

    handleClick = () => {
        this.props.updateTraining(this.state.newTrainingModel);
        if (this.props.activeStep === 3) {
            this.props.reset();
            this.props.history.push("/honoplay/trainingseriesdetail");
        }
    }

    render() {
        const { updateError, loadingUpdate } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <Stepper handleNext={this.handleClick}
                            activeStep={this.props.activeStep}
                            handleBack={this.handleBack}
                            loading={loadingUpdate}
                        >
                            <TrainingUpdate trainingId={this.trainingId}
                                basicTrainingUpdateModel={model => {
                                    if (model) {
                                        this.setState({
                                            newTrainingModel: model,
                                            updateError: false
                                        });
                                    }
                                }}
                                updateError={updateError}
                            />
                            <Classrooms trainingId={this.trainingId} />
                            <Session classroomId={this.props.classroomId} />
                            <Summary trainingId={this.trainingId} />
                        </Stepper>
                    </Grid>
                </Grid>
            </div>
        );
    }
}

const mapStateToProps = state => {
    const {
        isUpdateTrainingLoading,
        updateTraining,
        errorUpdateTraining
    } = state.updateTraining;

    let activeStep = state.ActiveStep.activeStep
    let classroomId = state.ClassroomId.id

    return {
        isUpdateTrainingLoading,
        updateTraining,
        errorUpdateTraining,
        activeStep,
        classroomId,
    };
};

const mapDispatchToProps = {
    updateTraining,
    increment,
    decrement,
    reset,
    changeId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSeriesInformation));