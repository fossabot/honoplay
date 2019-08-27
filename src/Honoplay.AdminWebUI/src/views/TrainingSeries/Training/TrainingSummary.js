import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import moment from 'moment';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress, Typography } from '@material-ui/core';
import Style from '../../Style';
import InfoCard from '../../../components/Card/InfoCard';
import Edit from '@material-ui/icons/Edit';
import { connect } from "react-redux";
import { fetchTraining } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";

const GridItem = ({ xs = 12, sm = 10, label, value }) => {
    return (
        <>
            <Grid item xs={xs} sm={sm}>
                <Typography gutterBottom component="p">
                    {`${label}${':'}`}
                </Typography>
            </Grid>
            <Grid item xs={12} sm={12 - sm}>
                <Typography gutterBottom component="p">
                    {value}
                </Typography>
            </Grid>
        </>
    );
};

class TrainingSummary extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            training: [
                {
                    trainingSeriesId: '',
                    trainingCategoryId: '',
                    name: '',
                    description: '',
                    beginDateTime: '',
                    endDateTime: ''
                }
            ],
            loadingTraining: false,
        };
    }

    trainingId = null;

    componentDidUpdate(prevProps) {
        const {
            isTrainingLoading,
            training,
            errorTraining
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
    }

    componentDidMount() {
        this.props.fetchTraining(this.trainingId);
    }




    render() {
        const { loadingClassroom, training } = this.state;
        const { classes, trainingId } = this.props;

        this.trainingId = trainingId;

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
                            <InfoCard
                                titleName={translate('TrainingInformation')}
                                icon={<Edit />}
                            >
                                <Grid container spacing={8}>
                                    <GridItem sm={2} label={translate('TrainingName')} value={training.name} />
                                    <GridItem sm={2} label={translate('TrainingCategory')} value='Yazılım' />
                                    <GridItem sm={2} label={translate('BeginDate')} value={moment(training.beginDateTime).format("DD/MM/YYYY")} />
                                    <GridItem sm={2} label={translate('EndDate')} value={moment(training.endDateTime).format("DD/MM/YYYY")} />
                                    <GridItem sm={2} label={translate('Description')} value={training.description} />
                                    <Grid item xs={12} sm={12} />
                                </Grid>
                            </InfoCard>
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

    return {
        isTrainingLoading,
        training,
        errorTraining
    };
};

const mapDispatchToProps = {
    fetchTraining
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TrainingSummary));