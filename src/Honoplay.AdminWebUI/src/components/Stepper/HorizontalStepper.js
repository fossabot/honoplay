import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
    Stepper,
    Step,
    StepLabel,
    Button,
    MuiThemeProvider,
    Grid,
    CircularProgress
} from '@material-ui/core';
import { Style, theme } from './Style';

function getSteps() {
    return [translate('TrainingInformation'), translate('Classroom'), translate('Summary&Confirmation')];
}

class HorizontalStepper extends React.Component {


    render() {
        const { classes, children, handleNext, handleBack, handleReset, activeStep, loading } = this.props;
        const steps = getSteps();

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
                                <div>
                                    <Button
                                        disabled={activeStep === 0}
                                        onClick={handleBack}
                                        className={classes.backButton}
                                    >
                                        {translate('Back')}
                                    </Button>
                                    <Button
                                        variant="contained"
                                        disabled={loading}
                                        color="secondary"
                                        onClick={handleNext}
                                        className={classes.nextButton}>
                                        {activeStep === steps.length - 1 ? translate('Save') : translate('NextStep')}
                                    </Button>
                                    {loading && (
                                        <CircularProgress
                                            size={24}
                                            disableShrink={true}
                                            className={classes.buttonProgress}
                                        />
                                    )}
                                </div>

                            </div>
                        </Grid>
                    </Grid>
                </div>
            </MuiThemeProvider>
        );
    }
}


export default withStyles(Style)(HorizontalStepper);
