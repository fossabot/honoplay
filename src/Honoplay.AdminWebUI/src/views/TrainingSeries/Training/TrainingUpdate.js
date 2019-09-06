import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import moment from 'moment';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';

import { connect } from "react-redux";
import { fetchTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";


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
                { id: 1, name: 'Yazılım', },
            ],
        };
    }

    trainingId = null;

    componentDidUpdate(prevProps) {
        const {
            isTrainingLoading,
            training,
            errorTraining,
        } = this.props;

        if (prevProps.isTrainingLoading && !isTrainingLoading) {
            this.setState({
                loadingTraining: true
            })
        }
        if (prevProps.isTrainingLoading && !isTrainingLoading && training) {
            this.props.basicTrainingUpdateModel(training.items[0]);
            if (!errorTraining) {
                this.setState({
                    training: training.items[0],
                    loadingTraining: false
                })
                
            }
        }
    }

    componentDidMount() {
        this.props.fetchTraining(this.trainingId);
    }

    render() {
        const { loadingClassroom, training, trainingCategory } = this.state;
        const { classes, trainingId, updateError } = this.props;

        this.trainingId = trainingId;
        return (

            <div className={classes.root}>
                {loadingClassroom ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={3}>
                        <Grid item xs={12} sm={12}>
                            <Input
                                error={updateError}
                                labelName={translate('TrainingName')}
                                inputType="text"
                                value={training.name}
                                onChange={e => {
                                    training.name = e.target.value;
                                    this.props.basicTrainingUpdateModel(this.state.training);
                                }}
                            />
                            <DropDown
                                error={updateError}
                                labelName={translate('TrainingCategory')}
                                data={trainingCategory}
                                value={training.trainingCategoryId}
                                onChange={e => {
                                    training.trainingCategoryId = e.target.value;
                                    this.props.basicTrainingUpdateModel(this.state.training);
                                }}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('BeginDate')}
                                inputType="date"
                                value={moment(training.beginDateTime).format("YYYY-MM-DD")}
                                onChange={e => {
                                    training.beginDateTime = e.target.value;
                                    this.props.basicTrainingUpdateModel(this.state.training);
                                }}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('EndDate')}
                                inputType="date"
                                value={moment(training.endDateTime).format("YYYY-MM-DD")}
                                onChange={e => {
                                    training.endDateTime = e.target.value;
                                    this.props.basicTrainingUpdateModel(this.state.training);
                                }}
                            />
                            <Input
                                error={updateError}
                                multiline
                                labelName={translate('Description')}
                                inputType="text"
                                value={training.description}
                                onChange={e => {
                                    training.description = e.target.value;
                                    this.props.basicTrainingUpdateModel(this.state.training);
                                }}
                            />
                        </Grid>
                        <Grid item xs={12} sm={12} />
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

    const data = training;

    return {
        isTrainingLoading,
        training: data,
        errorTraining,
    };
};

const mapDispatchToProps = {
    fetchTraining,
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingUpdate));