import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';


class SessionCreate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            sessionLoading: false,
        }
    }

    render() {
        const { sessionLoading } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={12}>
                        <Input
                            labelName={translate('SessionName')}
                            inputType="text"
                        />
                        <DropDown
                            labelName={translate('Game')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            onClick={this.handleClick}
                            disabled={sessionLoading}
                        />
                        {sessionLoading && (
                            <CircularProgress
                                size={24}
                                disableShrink={true}
                                className={classes.buttonProgress}
                            />
                        )}
                    </Grid>
                </Grid>
            </div >
        );
    }
}

export default withStyles(Style)(SessionCreate);