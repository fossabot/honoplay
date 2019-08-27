import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import moment from 'moment';
import { withStyles } from '@material-ui/core/styles';
import classNames from "classnames";
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { fetchTraining, updateTraining, fetchTrainingListByTrainingSeriesId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";


class TrainingUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            training:
            {
                trainingSeriesId: '',
                trainingCategoryId: '',
                name: '',
                description: '',
                beginDateTime: '',
                endDateTime: ''
            },
            loadingTraining: false,
            trainingCategory: [
                { id: 2, name: 'Yazılım', },
            ],
            success: false,
            loadingUpdate: false,
            updateError: false
        };
    }

    trainingId = null;
    trainingSeriesId = null;

    componentDidUpdate(prevProps) {
        const {
            isTrainingLoading,
            training,
            errorTraining,
            isUpdateTrainingLoading,
            updateTraining,
            errorUpdateTraining
        } = this.props;

        if (prevProps.isTrainingLoading && !isTrainingLoading) {
            this.setState({
                loadingTraining: true
            })
        }
        if (prevProps.isTrainingLoading && !isTrainingLoading && training) {
            if (!errorTraining) {
                this.setState({
                    training: training.items[0],
                })
            }
        }
        if (!prevProps.isUpdateTrainingLoading && isUpdateTrainingLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateTraining && errorUpdateTraining) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateTrainingLoading && !isUpdateTrainingLoading && updateTraining) {
            if (!errorUpdateTraining) {
                this.props.fetchTrainingListByTrainingSeriesId(this.trainingSeriesId);
                this.setState({
                    updateError: false,
                    loadingUpdate: false,
                    success: true,
                });
                setTimeout(() => {
                    this.setState({ success: false });
                }, 1000);
            }
        }
    }

    componentDidMount() {
        this.props.fetchTraining(this.trainingId);
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            training: {
                ...prevState.training,
                [name]: value
            },
        }))
    }

    handleClick = () => {
        this.props.updateTraining(this.state.training);
    }

    render() {
        const { loadingClassroom, training, trainingCategory, success, loadingUpdate, updateError } = this.state;
        const { classes, trainingId, trainingSeriesId } = this.props;

        this.trainingId = trainingId;
        this.trainingSeriesId = trainingSeriesId;

        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });

        return (

            <div className={classes.root}>
                {loadingClassroom ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={24}>
                        <Grid item xs={12} sm={12}>
                            <Input
                                error={updateError}
                                labelName={translate('TrainingName')}
                                inputType="text"
                                value={training.name}
                                onChange={this.handleChange}
                                name="name"
                            />
                            <DropDown
                                error={updateError}
                                labelName={translate('TrainingCategory')}
                                data={trainingCategory}
                                value={training.trainingCategoryId}
                                onChange={this.handleChange}
                                name="trainingCategoryId"
                            />
                            <Input
                                error={updateError}
                                labelName={translate('BeginDate')}
                                inputType="date"
                                value={moment(training.beginDateTime).format("YYYY-MM-DD")}
                                onChange={this.handleChange}
                                name="beginDateTime"
                            />
                            <Input
                                error={updateError}
                                labelName={translate('EndDate')}
                                inputType="date"
                                value={moment(training.endDateTime).format("YYYY-MM-DD")}
                                onChange={this.handleChange}
                                name="endDateTime"
                            />
                            <Input
                                error={updateError}
                                multiline
                                labelName={translate('Description')}
                                inputType="text"
                                value={training.description}
                                onChange={this.handleChange}
                                name="description"
                            />
                        </Grid>
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={11} />
                        <Grid item xs={12} sm={1}>
                            <Button
                                className={buttonClassname}
                                buttonColor="primary"
                                buttonName={translate('Update')}
                                onClick={this.handleClick}
                                disabled={loadingUpdate}
                            />
                            {loadingUpdate && (
                                <CircularProgress
                                    size={24}
                                    disableShrink={true}
                                    className={classes.buttonProgressUpdate}
                                />
                            )}
                        </Grid>
                    </Grid>
                }
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isTrainingLoading,
        training,
        errorTraining
    } = state.training;

    const {
        isUpdateTrainingLoading,
        updateTraining,
        errorUpdateTraining
    } = state.updateTraining;

    const data = training;

    return {
        isTrainingLoading,
        training: data,
        errorTraining,
        isUpdateTrainingLoading,
        updateTraining,
        errorUpdateTraining
    };
};

const mapDispatchToProps = {
    fetchTraining,
    updateTraining,
    fetchTrainingListByTrainingSeriesId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingUpdate));