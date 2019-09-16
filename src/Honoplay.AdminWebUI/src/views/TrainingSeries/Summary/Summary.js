import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import moment from 'moment';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Typography, Grid } from '@material-ui/core';
import Edit from '@material-ui/icons/Edit';
import Style from '../../Style';
import InfoCard from '../../../components/Card/InfoCard';
import Card from '../../../components/Card/CardComponents';
import Header from '../../../components/Typography/TypographyComponent';

import { connect } from 'react-redux';
import { fetchTraining } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training';
import { fetchClassroomListByTrainingId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom';

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

class Summary extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      training: {
        trainingSeriesId: '',
        trainingCategoryId: '',
        name: '',
        description: '',
        beginDateTime: '',
        endDateTime: ''
      },
      loadingTraining: false,
      classroomList: [],
      trainers: null
    };
  }

  trainingId = null;

  componentDidUpdate(prevProps) {
    const {
      isTrainingLoading,
      training,
      errorTraining,
      isClassroomListByTrainingIdLoading,
      classroomsListByTrainingId,
      errorClassroomListByTrainingId
    } = this.props;

    if (prevProps.isTrainingLoading && !isTrainingLoading) {
      this.setState({
        loadingTraining: true
      });
    }
    if (prevProps.isTrainingLoading && !isTrainingLoading && training) {
      if (!errorTraining) {
        this.setState({
          training: training.items[0]
        });
      }
    }

    if (
      !prevProps.errorClassroomListByTrainingId &&
      errorClassroomListByTrainingId
    ) {
      this.setState({
        classroomListError: true
      });
    }
    if (
      prevProps.isClassroomListByTrainingIdLoading &&
      !isClassroomListByTrainingIdLoading &&
      classroomsListByTrainingId
    ) {
      this.setState({
        classroomList: classroomsListByTrainingId.items
      });
    }
  }

  componentDidMount() {
    this.props.fetchTraining(this.trainingId);
    this.props.fetchClassroomListByTrainingId(this.trainingId);
  }

  render() {
    const { training, classroomList } = this.state;
    const { classes, trainingId } = this.props;

    this.trainingId = trainingId;

    return (
      <div className={classes.root}>
        <InfoCard titleName={translate('TrainingInformation')} icon={<Edit />}>
          <Grid container spacing={3}>
            <GridItem
              sm={2}
              label={translate('TrainingName')}
              value={training.name}
            />
            <GridItem
              sm={2}
              label={translate('TrainingCategory')}
              value="Yazılım"
            />
            <GridItem
              sm={2}
              label={translate('BeginDate')}
              value={moment(training.beginDateTime).format('DD/MM/YYYY')}
            />
            <GridItem
              sm={2}
              label={translate('EndDate')}
              value={moment(training.endDateTime).format('DD/MM/YYYY')}
            />
            <GridItem
              sm={2}
              label={translate('Description')}
              value={training.description}
            />
            <Grid item xs={12} sm={12} />
          </Grid>
        </InfoCard>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Header pageHeader={translate('Classrooms')} />
          </Grid>
          {classroomList.length === 0 && (
            <Grid item xs={12} sm={12}>
              <Typography gutterBottom component="p">
                {translate('NoRecordsToDisplay')}
              </Typography>
            </Grid>
          )}
          <Grid item xs={12} sm={9}>
            <Card data={classroomList} cardInfo iconName="users" />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const { isTrainingLoading, training, errorTraining } = state.training;

  const {
    isClassroomListByTrainingIdLoading,
    classroomsListByTrainingId,
    errorClassroomListByTrainingId
  } = state.classroomListByTrainingId;

  return {
    isTrainingLoading,
    training,
    errorTraining,
    isClassroomListByTrainingIdLoading,
    classroomsListByTrainingId,
    errorClassroomListByTrainingId
  };
};

const mapDispatchToProps = {
  fetchTraining,
  fetchClassroomListByTrainingId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Summary));
