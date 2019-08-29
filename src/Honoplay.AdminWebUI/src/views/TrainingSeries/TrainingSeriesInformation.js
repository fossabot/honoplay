import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import Training from './Training/Training';
import Classroom from './Classroom/Classroom';
import Summary from './Summary/Summary';
import Session from './Session/Session';


import { connect } from "react-redux";
import { createTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";
import { increment, decrement, reset } from '../../redux/actions/ActiveStepActions';
import { changeId } from '../../redux/actions/ClassroomIdActions';


class TrainingSeriesInformation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            newTrainingModel: null,
            trainingLoading: false,
            trainingError: false,
            trainingId: null,
        }
    }

    trainingModel = {
        createTrainingModels: [

        ]
    }

    componentDidUpdate(prevProps) {
        const {
            isCreateTrainingLoading,
            newCreateTraining,
            errorCreateTraining
        } = this.props;

        if (!prevProps.isCreateTrainingLoading && isCreateTrainingLoading) {
            this.setState({
                trainingLoading: true
            })
        }
        if (!prevProps.errorCreateTraining && errorCreateTraining) {
            this.setState({
                trainingError: true,
                trainingLoading: false
            })
        }
        if (prevProps.isCreateTrainingLoading && !isCreateTrainingLoading && newCreateTraining) {
            if (!errorCreateTraining) {
                newCreateTraining.items[0].map((training) => {
                    this.setState({ trainingId: training.id })
                })
                this.setState({
                    trainingLoading: false,
                    trainingError: false,
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

    handleClick = () => {
        if (this.props.activeStep === 0) {
            this.trainingModel.createTrainingModels = this.state.newTrainingModel;
            const { createTraining } = this.props;
            createTraining(this.trainingModel);
        } else if (this.props.activeStep === 2) {
            this.props.increment(this.props.activeStep);
        } else if (this.props.activeStep === 3) {
            this.props.history.push("/honoplay/trainingseriesdetail");
            this.props.reset();
        }
    }

    trainingSeriesId = localStorage.getItem("trainingSeriesId");

    render() {
        const { trainingError, trainingLoading, trainingId } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={16}>
                    <Grid item xs={12} sm={12}>
                        <Stepper handleNext={this.handleClick}
                            activeStep={this.props.activeStep}
                            handleBack={this.handleBack}
                            loading={trainingLoading}
                        >
                            <Training trainingSeriesId={this.trainingSeriesId}
                                basicTrainingModel={model => {
                                    if (model) {
                                        this.setState({
                                            newTrainingModel: model,
                                            trainingError: false,
                                        });
                                    }
                                }}
                                trainingError={trainingError}
                            />
                            <Classroom trainingId={trainingId} />
                            <Session classroomId={this.props.classroomId}/>
                            <Summary trainingId={trainingId} />
                        </Stepper>
                    </Grid>
                </Grid>
            </div>
        );
    }
}

const mapStateToProps = state => {

    const {
        isCreateTrainingLoading,
        createTraining,
        errorCreateTraining
    } = state.createTraining;

    let newCreateTraining = createTraining;
    let activeStep = state.ActiveStep.activeStep
    let classroomId = state.ClassroomId.id

    return {
        isCreateTrainingLoading,
        newCreateTraining,
        errorCreateTraining,
        activeStep,
        classroomId
    };
};

const mapDispatchToProps = {
    createTraining,
    increment,
    decrement,
    reset,
    changeId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSeriesInformation));