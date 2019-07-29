import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Button from '../../components/Button/ButtonComponent';
import OptionsComponent from '../../components/Options/OptionsComponent';

class Options extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            option: 3,
        }
    }
    handleClick = () => {
        this.setState({ 
            option: this.state.option + 1,
        })
    }
    render() {
        const { option } = this.state;
        const { classes } = this.props;
        const options = [];
        for ( var i=0; i<option; i++) {
            options.push(<OptionsComponent key={i}/>)
        }
        return (
            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={12}>
                    {options}              
                    </Grid> 
                    <Grid item xs={12} sm={12} >
                        <Button
                            buttonColor="secondary"
                            buttonName={translate('AddOption')}
                            onClick={this.handleClick}
                            buttonIcon="plus"
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }
}

export default withStyles(Style)(Options);