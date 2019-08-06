import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import Training from '../TrainingSeries/Training';
import Classroom from '../TrainingSeries/Classroom';

class TrainingSeriesInformation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
        }
    }

    render() {
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={16}>
                    <Grid item xs={12} sm={12}>
                        <Stepper>
                            <Training/>
                            <Classroom/>
                        </Stepper>
                    </Grid>
                </Grid>
            </div>
        );
    }
}


export default withStyles(Style)(TrainingSeriesInformation);