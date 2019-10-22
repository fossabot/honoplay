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
  createQuestionCategory,
  fetchQuestionCategoryList
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/QuestionCategory';

class QuestionCategory extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loadingQuestionCategory: false,
      questionCategories: [],
      questionCategoryError: false,
      questionCategory: {
        createQuestionCategoryModels: [
          {
            name: ''
          }
        ]
      }
    };
  }

  componentDidUpdate(prevProps) {
    const {
      isQuestionCategoryListLoading,
      questionCategories,
      errorQuestionCategoryList,
      isCreateQuestionCategoryLoading,
      newQuestionCategory,
      errorCreateQuestionCategory
    } = this.props;

    if (
      prevProps.isQuestionCategoryListLoading &&
      !isQuestionCategoryListLoading &&
      questionCategories
    ) {
      if (!errorQuestionCategoryList) {
        this.setState({
          questionCategories: questionCategories.items
        });
      }
    }
    if (
      !prevProps.isCreateQuestionCategoryLoading &&
      isCreateQuestionCategoryLoading
    ) {
      this.setState({
        loadingQuestionCategory: true
      });
    }
    if (!prevProps.errorCreateQuestionCategory && errorCreateQuestionCategory) {
      this.setState({
        questionCategoryError: true,
        loadingQuestionCategory: false
      });
    }
    if (
      prevProps.isCreateQuestionCategoryLoading &&
      !isCreateQuestionCategoryLoading &&
      newQuestionCategory
    ) {
      this.props.fetchQuestionCategoryList(0, 50);
      if (!errorCreateQuestionCategory) {
        this.setState({
          questionCategoryError: false,
          loadingQuestionCategory: false
        });
      }
    }
  }

  componentDidMount() {
    this.props.fetchQuestionCategoryList(0, 50);
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      questionCategory: {
        ...prevState.questionCategory,
        createQuestionCategoryModels: [{ [name]: value }]
      },
      questionCategoryError: false
    }));
  };

  handleClick = () => {
    this.props.createQuestionCategory(this.state.questionCategory);
  };

  render() {
    const {
      loadingQuestionCategory,
      questionCategories,
      questionCategoryError
    } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Input
              error={questionCategoryError}
              onChange={this.handleChange}
              labelName={translate('QuestionCategory')}
              inputType="text"
              name="name"
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              onClick={this.handleClick}
              buttonColor="secondary"
              buttonName={translate('Add')}
              disabled={loadingQuestionCategory}
            />
            {loadingQuestionCategory && (
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
            <Chip data={questionCategories}></Chip>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isQuestionCategoryListLoading,
    questionCategories,
    errorQuestionCategoryList
  } = state.questionCategoryList;

  const {
    isCreateQuestionCategoryLoading,
    newQuestionCategory,
    errorCreateQuestionCategory
  } = state.createQuestionCategory;

  return {
    isQuestionCategoryListLoading,
    questionCategories,
    errorQuestionCategoryList,
    isCreateQuestionCategoryLoading,
    newQuestionCategory,
    errorCreateQuestionCategory
  };
};

const mapDispatchToProps = {
  createQuestionCategory,
  fetchQuestionCategoryList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(QuestionCategory));
