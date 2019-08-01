import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Typography } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Header from '../../components/Typography/TypographyComponent';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

class TrainingSeriesCreate extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { classes } = this.props;
        return (
            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={12}>
                        <Header
                            pageHeader={translate('CreateATrainingSeries')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12}>
                        <Typography>
                            {translate('YouCanCreateTrainingSetsAndCollectDifferentTrainingsInOneField')}
                        </Typography>
                    </Grid>
                    <Grid item xs={12} sm={12}/>
                    <Grid item xs={12} sm={12}>
                        <Input
                            labelName={translate('Name')}
                            inputType="text"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}/>
                    <Grid item xs={12} sm={10}/>
                    <Grid item xs={12} sm={2}>
                        <Button
                            buttonIcon="plus"
                            buttonColor="primary"
                            buttonName={translate('CreateATrainingSeries')}
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }
}

export default withStyles(Style)(TrainingSeriesCreate);