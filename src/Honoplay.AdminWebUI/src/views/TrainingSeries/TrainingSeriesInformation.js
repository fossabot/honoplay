import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import Training from '../TrainingSeries/Training';
import Classroom from '../TrainingSeries/Classroom';


import { connect } from "react-redux";
import { createTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";

class TrainingSeriesInformation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            newTrainingModel: null,
            trainingLoading: false,
            trainingError: false,
            activeStep: 1
        }
    }

    trainingModel = {
        createTrainingModels: [

        ]
    }

    componentDidUpdate(prevProps) {
        const {
            isCreateTrainingLoading,
            createTraining,
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
        if (prevProps.isCreateTrainingLoading && !isCreateTrainingLoading && createTraining) {
            if (!errorCreateTraining) {
                this.setState({
                    trainingLoading: false,
                    trainingError: false,
                    activeStep: 1
                });
            }
        }

    }

    handleBack = () => {
        this.setState(state => ({
            activeStep: state.activeStep - 1,
        }));
    }

    handleReset = () => {
        this.setState({
            activeStep: 0,
        });
    }

    handleClick = () => {
        this.trainingModel.createTrainingModels = this.state.newTrainingModel;
        const { createTraining } = this.props;
        createTraining(this.trainingModel);
    }

    trainingSeriesId = this.props.match.params.trainingseriesId;

    render() {
        const { activeStep, trainingError, trainingLoading } = this.state;
        const { classes } = this.props;
        return (

            <div className={classes.root}>
                <Grid container spacing={16}>
                    <Grid item xs={12} sm={12}>
                        <Stepper handleNext={this.handleClick}
                            activeStep={activeStep}
                            handleBack={this.handleBack}
                            handleReset={this.handleReset}
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
                            <Classroom />
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

    return {
        isCreateTrainingLoading,
        createTraining,
        errorCreateTraining
    };
};

const mapDispatchToProps = {
    createTraining
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSeriesInformation));