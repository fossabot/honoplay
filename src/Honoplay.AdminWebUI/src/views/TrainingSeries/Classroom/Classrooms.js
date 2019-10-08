import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Grid,
  Divider,
  TextField,
  InputAdornment,
  IconButton
} from '@material-ui/core';
import Style from '../../Style';
import Card from '../../../components/Card/Card';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import Button from '../../../components/Button/ButtonComponent';
import SearchIcon from '@material-ui/icons/Search';

import { connect } from 'react-redux';
import { fetchClassroomListByTrainingId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom';
import { increment, decrement } from '../../../redux/actions/ActiveStepActions';
import { changeId } from '../../../redux/actions/ClassroomIdActions';

class Classrooms extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      search: [],
      openDialog: false,
      classroomListError: false,
      classroomList: [],
      classroomId: null,
      openDialog: false
    };
  }

  trainingId = localStorage.getItem('trainingId');

  componentDidUpdate(prevProps) {
    const {
      isClassroomListByTrainingIdLoading,
      classroomsListByTrainingId,
      errorClassroomListByTrainingId
    } = this.props;

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

  handleClickOpenDialog = () => {
    this.setState({ openDialog: true });
  };

  handleCloseDialog = () => {
    this.setState({ openDialog: false });
  };

  componentDidMount() {
    this.props.fetchClassroomListByTrainingId(this.trainingId);
  }

  onSearchInputChange = event => {
    let searched = [];
    this.state.classroomList.map((classroom, index) => {
      if (classroom.name.includes(event.target.value)) {
        searched = searched.concat(classroom);
      }
    });
    this.setState({ search: searched });
  };

  handleClick = () => {
    this.props.history.push(
      `/admin/trainingseries/training/${this.props.match.params.trainingName}/classroom`
    );
  };

  render() {
    const { classroomList, search } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={9}>
            <BreadCrumbs />
          </Grid>
          <Grid item xs={12} sm={2}>
            <TextField
              placeholder={translate('Search')}
              margin="none"
              onChange={this.onSearchInputChange}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">
                    <IconButton>
                      <SearchIcon />
                    </IconButton>
                  </InputAdornment>
                )
              }}
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Add')}
              onClick={this.handleClick}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Card
              data={search.length === 0 ? classroomList : search}
              url="training/classroom"
              id={id => {
                if (id) {
                  localStorage.setItem('classroomId', id);
                }
              }}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isClassroomListByTrainingIdLoading,
    classroomsListByTrainingId,
    errorClassroomListByTrainingId
  } = state.classroomListByTrainingId;

  let activeStep = state.ActiveStep.activeStep;
  let id = state.ClassroomId.id;

  return {
    isClassroomListByTrainingIdLoading,
    classroomsListByTrainingId,
    errorClassroomListByTrainingId,
    activeStep,
    id
  };
};

const mapDispatchToProps = {
  fetchClassroomListByTrainingId,
  increment,
  decrement,
  changeId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Classrooms));
