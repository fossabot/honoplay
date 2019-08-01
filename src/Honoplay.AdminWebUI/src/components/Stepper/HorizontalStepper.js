import React, { Children } from 'react';
import { withStyles } from '@material-ui/core/styles';
import {
    Stepper,
    Step,
    StepLabel,
    Button,
    MuiThemeProvider
} from '@material-ui/core';
import { Style, theme } from './Style';

function getSteps() {
    return ['Select master blaster campaign settings', 'Create an ad group', 'Create an ad'];
}

class HorizontalStepper extends React.Component {
    state = {
        activeStep: 0,
    };

    handleNext = () => {
        this.setState(state => ({
            activeStep: state.activeStep + 1,
        }));
    };

    handleBack = () => {
        this.setState(state => ({
            activeStep: state.activeStep - 1,
        }));
    };

    handleReset = () => {
        this.setState({
            activeStep: 0,
        });
    };

    render() {
        const { classes, children } = this.props;
        const steps = getSteps();
        const { activeStep } = this.state;

        return (
            <MuiThemeProvider theme={theme}>
                <div className={classes.root}>
                    <Stepper activeStep={activeStep} alternativeLabel
                        className={classes.stepper}>
                        {steps.map(label => (
                            <Step key={label}>
                                <StepLabel>{label}</StepLabel>
                            </Step>
                        ))}
                    </Stepper>
                    {children}
                    <div>
                        {this.state.activeStep === steps.length ? (
                            <div>
                                <Button onClick={this.handleReset}>Reset</Button>
                            </div>
                        ) : (
                                <div>
                                    <div>
                                        <Button
                                            disabled={activeStep === 0}
                                            onClick={this.handleBack}
                                            className={classes.backButton}
                                        >
                                            Back
                                        </Button>
                                        <Button
                                            variant="contained"
                                            color="secondary"
                                            onClick={this.handleNext}
                                            className={classes.nextButton}>
                                            {activeStep === steps.length - 1 ? 'Finish' : 'Next'}
                                        </Button>
                                    </div>
                                </div>
                            )}
                    </div>
                </div>
            </MuiThemeProvider>
        );
    }
}


export default withStyles(Style)(HorizontalStepper);
