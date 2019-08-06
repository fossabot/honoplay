import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Stepper from '../../components/Stepper/HorizontalStepper';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';


import { connect } from "react-redux";

class Training extends React.Component {

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
                        <Grid item xs={12} sm={12}>
                            <Input
                                labelName={translate('TrainingName')}
                                inputType="text"
                            />
                            <DropDown
                                labelName={translate('TrainingCategory')}
                            />
                            <Input
                                labelName={translate('BeginDate')}
                                inputType="date"
                            />
                            <Input
                                labelName={translate('EndDate')}
                                inputType="date"
                            />
                            <Input
                                multiline
                                labelName={translate('Description')}
                                inputType="text"
                            />
                        </Grid>
                    </Grid>
                </Grid>
            </div>
        );
    }
}


const mapStateToProps = state => {

    return {

    };
};

const mapDispatchToProps = {
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Training));