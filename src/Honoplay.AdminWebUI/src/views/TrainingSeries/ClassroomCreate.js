import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { fetchTrainersList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer";

class ClassroomCreate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            trainer: null,
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isTrainerListLoading,
            errorTrainerList,
            trainersList,
        } = this.props;

        if (prevProps.isTrainerListLoading && !isTrainerListLoading && trainersList) {
            if (!errorTrainerList) {
                this.setState({
                    trainer: trainersList.items,
                });
            }
        }
    }
    
    componentDidMount() {
        this.props.fetchTrainersList(0, 50);
    }

    render() {
        const { loading, trainer } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={12}>
                        <Input
                            labelName={translate('ClassroomName')}
                            inputType="text"
                        />
                        <DropDown
                            data={trainer}
                            labelName={translate('Trainer')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            onClick={this.handleClick}
                            disabled={loading}
                        />
                        {loading && (
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

const mapStateToProps = state => {

    const {
        isTrainerListLoading,
        errorTrainerList,
        trainersList
    } = state.trainersList;

    return {
        isTrainerListLoading,
        errorTrainerList,
        trainersList,
    };
};

const mapDispatchToProps = {
    fetchTrainersList,
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(ClassroomCreate));