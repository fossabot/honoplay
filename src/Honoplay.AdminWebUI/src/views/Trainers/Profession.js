import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Chip from '../../components/Chip/ChipComponent';

import { connect } from 'react-redux';
import {
  createProfession,
  fetchProfessionList
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Profession';

class Profession extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      professions: [],
      professionError: false,
      loadingProfession: false
    };
  }

  professionsModel = {
    professions: []
  };

  componentDidUpdate(prevProps) {
    const {
      isCreateProfessionLoading,
      errorCreateProfession,
      newProfession,
      isProfessionListLoading,
      professionList
    } = this.props;

    if (
      prevProps.isProfessionListLoading &&
      !isProfessionListLoading &&
      professionList
    ) {
      this.setState({
        professions: professionList.items
      });
    }
    if (!prevProps.isCreateProfessionLoading && isCreateProfessionLoading) {
      this.setState({
        loadingProfession: true
      });
    }
    if (!prevProps.errorCreateProfession && errorCreateProfession) {
      this.setState({
        professionError: true,
        loadingProfession: false
      });
    }
    if (
      prevProps.isCreateProfessionLoading &&
      !isCreateProfessionLoading &&
      newProfession
    ) {
      this.props.fetchProfessionList(0, 50);
      if (!errorCreateProfession) {
        this.setState({
          professionError: false,
          loadingProfession: false
        });
      }
    }
  }

  componentDidMount() {
    const { fetchProfessionList } = this.props;
    fetchProfessionList(0, 50);
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.professionsModel[name] = [value];
    this.setState({
      professionError: false
    });
  };

  handleClickProfession = () => {
    this.props.createProfession(this.professionsModel);
  };

  render() {
    const { professionError, loadingProfession, professions } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Input
              error={professionError}
              onChange={this.handleChange}
              labelName={translate('TrainerExpertise')}
              inputType="text"
              name="professions"
              value={this.professionsModel.professions}
              htmlFor="profession"
              id="profession"
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              onClick={this.handleClickProfession}
              buttonColor="secondary"
              buttonName={translate('Add')}
              disabled={loadingProfession}
            />
            {loadingProfession && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgress}
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Chip data={professions}></Chip>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateProfessionLoading,
    errorCreateProfession,
    newProfession
  } = state.professionCreate;

  const {
    isProfessionListLoading,
    professionList,
    errorProfessionList
  } = state.professionList;

  return {
    isCreateProfessionLoading,
    errorCreateProfession,
    newProfession,
    isProfessionListLoading,
    professionList,
    errorProfessionList
  };
};

const mapDispatchToProps = {
  createProfession,
  fetchProfessionList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Profession));
