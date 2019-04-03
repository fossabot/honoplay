import React, { Children } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { MuiThemeProvider, createMuiTheme, Button, 
         StepLabel, Step, Stepper, Grid} from '@material-ui/core';
import Style from './Style';

const muiTheme = createMuiTheme({
    overrides: {
        MuiStepIcon: {
            root: {
                color: '#e92428', 
                '&$active': {
                    color: '#e92428',
                },
                '&$completed': {
                    color: '#e92428',
                },
            },
        },
    }
});

function getSteps() {
  return ['Eğitim Bilgileri', 'Sınıflar', 'Oyun', 'Özet & Onay'];
}

class StepperComponent extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
        activeStep: 0,
      }; 
      this.handleNext=this.handleNext.bind(this);
      this.handleBack=this.handleBack.bind(this);
  }

  handleNext()
  {
    this.setState(state => ({
      activeStep: state.activeStep + 1,
    }));
  };

  handleBack() 
  {
    this.setState(state => ({
      activeStep: state.activeStep - 1,
    }));
  };

  render() {
    const { classes, children } = this.props;
    const steps = getSteps();
    const { activeStep } = this.state;

    return (
      <MuiThemeProvider theme={muiTheme}>
        <div className={classes.root}>
            <Grid container spacing={24}>
                <Grid item xs={12}>
                    <Stepper activeStep={activeStep} alternativeLabel className={classes.background}>
                    {steps.map(label => (
                        <Step key={label}>
                        <StepLabel>{label}</StepLabel>
                        </Step>
                    ))}
                    </Stepper>
                </Grid>
                <Grid item xs={12}>
                   {children}
                </Grid>
                <Grid item xs={12}>
                    <div>
                    {this.state.activeStep === steps.length ? (
                        <div></div>
                    ) : (
                        <div className={classes.location}>
                        <div>
                            <Button
                            disabled={activeStep === 0}
                            onClick={this.handleBack.bind(this)}
                            className={classes.backButton}
                            variant="contained"
                            color="default"
                            >
                            Geri Dön
                            </Button>
                            <Button variant="contained" color="secondary" onClick={this.handleNext.bind(this)} >
                            {activeStep === steps.length - 1 ? 'Kaydet' : 'Sonraki Adıma Devam Et'}
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

StepperComponent.propTypes = {
  classes: PropTypes.object,
};

export default withStyles(Style)(StepperComponent);