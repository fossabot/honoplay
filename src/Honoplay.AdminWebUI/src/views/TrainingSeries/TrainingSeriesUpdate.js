import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import TrainingUpdate from './Training/TrainingUpdate';
import Classrooms from './Classroom/Classrooms';


import { connect } from "react-redux";
import { updateTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";

class TrainingSeriesInformation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            activeStep: 0,
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
        console.log(this.state.newTrainingModel);
        this.props.updateTraining(this.state.newTrainingModel);
    }


    render() {
        const { activeStep, updateError, loadingUpdate } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={16}>
                    <Grid item xs={12} sm={12}>
                        <Stepper handleNext={this.handleClick}
                            activeStep={activeStep}
                            handleBack={this.handleBack}
                            handleReset={this.handleReset}
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

    return {
        isUpdateTrainingLoading,
        updateTraining,
        errorUpdateTraining
    };
};

const mapDispatchToProps = {
    updateTraining
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSeriesInformation));