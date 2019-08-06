import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
    Stepper,
    Step,
    StepLabel,
    Button,
    MuiThemeProvider,
    Grid
} from '@material-ui/core';
import { Style, theme } from './Style';

function getSteps() {
    return [translate('TrainingInformation'), translate('Classroom'), translate('Game'), translate('Summary&Confirmation')];
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
                    <Grid container spacing={40}>
                        <Grid item xs={12} sm={12}>
                            <Stepper activeStep={activeStep} alternativeLabel
                                className={classes.stepper}>
                                {steps.map(label => (
                                    <Step key={label}>
                                        <StepLabel>{label}</StepLabel>
                                    </Step>
                                ))}
                            </Stepper>
                        </Grid>
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={12}>
                            {children[activeStep]}
                        </Grid>
                        <Grid item xs={12} sm={10} />
                        <Grid item xs={12} sm={2}>
                            <div>
                                {this.state.activeStep === steps.length ? (
                                    <div>
                                        <Button onClick={this.handleReset}>{translate('Reset')}</Button>
                                    </div>
                                ) : (
                                        <div>
                                            <div>
                                                <Button
                                                    disabled={activeStep === 0}
                                                    onClick={this.handleBack}
                                                    className={classes.backButton}
                                                >
                                                    {translate('Back')}
                                        </Button>
                                                <Button
                                                    variant="contained"
                                                    color="secondary"
                                                    onClick={this.handleNext}
                                                    className={classes.nextButton}>
                                                    {activeStep === steps.length - 1 ? translate('Save') : translate('NextStep')}
                                                </Button>
                                            </div>
                                        </div>
                                    )}
                            </div>
                        </Grid>
                    </Grid>
                </div>
            </MuiThemeProvider>
        );
    }
}


export default withStyles(Style)(HorizontalStepper);
