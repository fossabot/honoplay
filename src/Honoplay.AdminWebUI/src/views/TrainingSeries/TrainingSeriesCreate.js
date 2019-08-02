import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { createTrainingSeries } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainingSeries";

class TrainingSeriesCreate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trainingError: false,
            loading: false,
            trainingSeries: {
                trainingSeriesModels: [
                    {
                        name: ''
                    }
                ]
            }
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isCreateTrainingSeriesLoading,
            createTrainingSeries,
            errorCreateTrainingSeries
        } = this.props;

        if (!prevProps.isCreateTrainingSeriesLoading && isCreateTrainingSeriesLoading) {
            this.setState({
                loading: true
            })
        }
        if (!prevProps.errorCreateTrainingSeries && errorCreateTrainingSeries) {
            this.setState({
                trainingError: true,
                loading: false
            })
        }
        if (prevProps.isCreateTrainingSeriesLoading && !isCreateTrainingSeriesLoading && createTrainingSeries) {
            if (!errorCreateTrainingSeries) {
                this.setState({
                    loading: false,
                    trainingError: false,
                });
            }
        }

    }

    handleClick = () => {
        console.log('state', this.state.trainingSeries);
        this.props.createTrainingSeries(this.state.trainingSeries);
    }

    render() {
        const { trainingSeries, trainingError, loading } = this.state;
        const { classes } = this.props;
        return (
            <div className={classes.root}>
                <Grid container spacing={24}>
                    {trainingSeries.trainingSeriesModels.map((training, id) => (
                        <Grid item xs={12} sm={12} key={id}>
                            <Input
                                error={trainingError}
                                labelName={translate('TrainingSeriesName')}
                                inputType="text"
                                onChange={e => training.name = e.target.value}
                            />
                        </Grid>
                    ))}
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
            </div>
        );
    }
}

const mapStateToProps = state => {
    const {
        isCreateTrainingSeriesLoading,
        createTrainingSeries,
        errorCreateTrainingSeries
    } = state.createTrainingSeries;


    console.log(createTrainingSeries);

    return {
        isCreateTrainingSeriesLoading,
        createTrainingSeries,
        errorCreateTrainingSeries
    };
};

const mapDispatchToProps = {
    createTrainingSeries
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSeriesCreate));