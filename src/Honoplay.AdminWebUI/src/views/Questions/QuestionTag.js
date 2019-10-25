import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import { translate } from '@omegabigdata/terasu-api-proxy';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Chip from '../../components/Chip/ChipComponent';
import Style from '../Style';

import { connect } from 'react-redux';
import {
  createTag,
  fetchTagList
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Tag';

class QuestionTag extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loadingQuestionTag: false,
      questionTags: [],
      questionTagError: false,
      questionTag: {
        createTagModels: [
          {
            name: '',
            toQuestion: true
          }
        ]
      }
    };
  }

  componentDidUpdate(prevProps) {
    const {
      isCreateTagLoading,
      newTag,
      errorCreateTag,
      isTagListLoading,
      tags,
      errorTagList
    } = this.props;

    if (prevProps.isTagListLoading && !isTagListLoading && tags) {
      if (!errorTagList) {
        this.setState({
          questionTags: tags.items
        });
      }
    }
    if (!prevProps.isCreateTagLoading && isCreateTagLoading) {
      this.setState({
        loadingQuestionTag: true
      });
    }
    if (!prevProps.errorCreateTag && errorCreateTag) {
      this.setState({
        questionTagError: true,
        loadingQuestionTag: false
      });
    }
    if (prevProps.isCreateTagLoading && !isCreateTagLoading && newTag) {
      this.props.fetchTagList(0, 50);
      if (!errorCreateTag) {
        this.setState({
          questionTagError: false,
          loadingQuestionTag: false
        });
      }
    }
  }

  componentDidMount() {
    this.props.fetchTagList(0, 50);
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      questionTag: {
        ...prevState.questionTag,
        createTagModels: [{ [name]: value, toQuestion: true }]
      },
      questionTagError: false
    }));
  };

  handleClick = () => {
    this.props.createTag(this.state.questionTag);
  };

  render() {
    const { loadingQuestionTag, questionTags, questionTagError } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Input
              error={questionTagError}
              onChange={this.handleChange}
              labelName={translate('QuestionTag')}
              inputType="text"
              name="name"
              htmlFor="tag"
              id="tag"
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              onClick={this.handleClick}
              buttonColor="secondary"
              buttonName={translate('Add')}
              disabled={loadingQuestionTag}
            />
            {loadingQuestionTag && (
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
            <Chip data={questionTags}></Chip>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const { isCreateTagLoading, newTag, errorCreateTag } = state.createTag;

  const { isTagListLoading, tags, errorTagList } = state.tagList;

  return {
    isCreateTagLoading,
    newTag,
    errorCreateTag,
    isTagListLoading,
    tags,
    errorTagList
  };
};

const mapDispatchToProps = {
  createTag,
  fetchTagList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(QuestionTag));
