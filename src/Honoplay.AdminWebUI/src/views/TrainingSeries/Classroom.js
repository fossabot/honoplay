import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';

class Classroom extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
        }
    }

    render() {
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={3}>
                        <CardButton
                            cardName={translate('AddNewClassroom')}
                            cardDescription={translate('YouCanCreateDifferentTrainingsForEachTrainingSeries')}
                            onClick={this.handleClick}
                        />
                    </Grid>
                    <Grid item xs={12} sm={9}></Grid>
                </Grid>
            </div >
        );
    }
}


export default withStyles(Style)(Classroom);