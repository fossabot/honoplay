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
import { fetchSessionListByClassroomId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session';

class Session extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      search: [],
      openDialog: false,
      sessionListError: false,
      sessionList: [],
      sessionId: null
    };
  }

  classroomId = localStorage.getItem('classroomId');

  componentDidUpdate(prevProps) {
    const {
      isSessionListByClassroomIdLoading,
      sessionListByClassroomId,
      errorSessionListByClassroomId
    } = this.props;

    if (
      !prevProps.errorSessionListByClassroomId &&
      errorSessionListByClassroomId
    ) {
      this.setState({
        sessionListError: true
      });
    }
    if (
      prevProps.isSessionListByClassroomIdLoading &&
      !isSessionListByClassroomIdLoading &&
      sessionListByClassroomId
    ) {
      this.setState({
        sessionList: sessionListByClassroomId.items
      });
    }
  }

  componentDidMount() {
    this.props.fetchSessionListByClassroomId(this.classroomId);
  }

  handleClickOpenDialog = () => {
    this.setState({ openDialog: true });
  };

  handleCloseDialog = () => {
    this.setState({ openDialog: false });
  };

  onSearchInputChange = event => {
    let searched = [];
    this.state.sessionList.map((session, index) => {
      if (session.name.includes(event.target.value)) {
        searched = searched.concat(session);
      }
    });
    this.setState({ search: searched });
  };

  handleClick = () => {
    this.props.history.push(
      `/admin/trainingseries/training/classroom/${this.props.match.params.classroomName}/session`
    );
  };

  render() {
    const { sessionList, search } = this.state;
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
              data={search.length === 0 ? sessionList : search}
              url="training/classroom"
              id={id => {}}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isSessionListByClassroomIdLoading,
    sessionListByClassroomId,
    errorSessionListByClassroomId
  } = state.sessionListByClassroomId;

  return {
    isSessionListByClassroomIdLoading,
    sessionListByClassroomId,
    errorSessionListByClassroomId
  };
};

const mapDispatchToProps = {
  fetchSessionListByClassroomId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Session));
