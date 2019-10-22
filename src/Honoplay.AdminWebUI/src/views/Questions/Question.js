import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Header from '../../components/Typography/TypographyComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Options from './Options';
import QuestionCategory from './QuestionCategory';

import { connect } from 'react-redux';
import {
  createQuestion,
  fetchQuestion,
  updateQuestion
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Question';
import {
  fetchOptionsByQuestionId,
  createOption,
  updateOption
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Options';
import { fetchQuestionDifficultyList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/QuestionDifficulty';
import { fetchQuestionCategoryList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/QuestionCategory';

class NewQuestion extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      questionsError: false,
      question: '',
      questionId: null,
      options: [],
      questionsModel: {
        difficultyId: '',
        categoryId: '',
        typeId: '',
        contentFileId: '',
        text: '',
        duration: ''
      },
      success: false,
      newOptions: [],
      questionDifficulty: [],
      questionCategory: []
    };
  }

  dataId = localStorage.getItem('dataid');

  index = null;

  optionsModel = {
    createOptionModels: []
  };

  componentDidUpdate(prevProps) {
    const {
      isCreateQuestionLoading,
      errorCreateQuestion,
      newQuestion,
      isQuestionLoading,
      question,
      errorQuestion,
      isUpdateQuestionLoading,
      updateQuestion,
      errorUpdateQuestion,
      isOptionListByQuestionIdLoading,
      optionsListByQuestionId,
      isCreateOptionLoading,
      createOption,
      errorCreateOption,
      isQuestionDifficultyListLoading,
      questionDifficulties,
      errorQuestionDifficultyList,
      isQuestionCategoryListLoading,
      questionCategories,
      errorQuestionCategoryList
    } = this.props;

    if (
      prevProps.isQuestionCategoryListLoading &&
      !isQuestionCategoryListLoading &&
      questionCategories
    ) {
      if (!errorQuestionDifficultyList) {
        this.setState({
          questionCategory: questionCategories.items
        });
      }
    }
    if (
      prevProps.isQuestionDifficultyListLoading &&
      !isQuestionDifficultyListLoading &&
      questionDifficulties
    ) {
      if (!errorQuestionDifficultyList) {
        this.setState({
          questionDifficulty: questionDifficulties.items
        });
      }
    }
    if (
      prevProps.isOptionListByQuestionIdLoading &&
      !isOptionListByQuestionIdLoading &&
      optionsListByQuestionId
    ) {
      this.setState({
        options: optionsListByQuestionId.items
      });
    }

    if (prevProps.isQuestionLoading && !isQuestionLoading && question) {
      if (!errorQuestion) {
        this.setState({
          questionsModel: question.items[0]
        });
      }
    }
    if (!prevProps.isCreateQuestionLoading && isCreateQuestionLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorCreateQuestion && errorCreateQuestion) {
      this.setState({
        questionsError: true,
        loading: false
      });
    }
    if (
      prevProps.isCreateQuestionLoading &&
      !isCreateQuestionLoading &&
      newQuestion
    ) {
      if (!errorCreateQuestion) {
        this.setState({
          question: newQuestion.items[0].text,
          questionId: newQuestion.items[0].id,
          loading: false,
          questionsError: false
        });
        this.props.createOption(this.state.newOptions);
      }
    }

    if (!prevProps.isUpdateQuestionLoading && isUpdateQuestionLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorUpdateQuestion && errorUpdateQuestion) {
      this.setState({
        questionsError: true,
        loading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateQuestionLoading &&
      !isUpdateQuestionLoading &&
      updateQuestion
    ) {
      if (!errorUpdateQuestion) {
        this.setState({
          questionsError: false,
          loading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
      this.state.newOptions.createOptionModels.map((option, id) => {
        if (option.id) {
          this.props.updateOption(option);
        } else {
          this.optionsModel.createOptionModels = [option];
          this.props.createOption(this.optionsModel);
        }
      });
    }

    if (!prevProps.isCreateOptionLoading && isCreateOptionLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorCreateOption && errorCreateOption) {
      this.setState({
        optionError: true,
        loading: false
      });
    }
    if (
      prevProps.isCreateOptionLoading &&
      !isCreateOptionLoading &&
      createOption
    ) {
      this.props.fetchOptionsByQuestionId(this.state.questionId);
      if (!errorCreateOption) {
        this.setState({
          loading: false,
          optionError: false
        });
      }
    }
  }

  componentDidMount() {
    if (this.dataId) {
      this.props.fetchQuestion(parseInt(this.dataId));
      this.props.fetchOptionsByQuestionId(this.dataId);
      this.props.fetchQuestionDifficultyList(0, 50);
      this.setState({
        questionId: this.dataId
      });
    }
    this.props.fetchQuestionDifficultyList(0, 50);
    this.props.fetchQuestionCategoryList(0, 50);
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      questionsModel: {
        ...prevState.questionsModel,
        [name]: value
      },
      questionsError: false
    }));
  };

  handleClick = () => {
    this.props.createQuestion(this.state.questionsModel);
  };

  handleUpdate = () => {
    this.props.updateQuestion(this.state.questionsModel);
    localStorage.removeItem('dataid');
  };

  render() {
    const {
      questionsError,
      loading,
      questionId,
      success,
      questionDifficulty,
      questionCategory
    } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Header pageHeader={translate('CreateAQuestion')} />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={this.dataId ? translate('Update') : translate('Save')}
              onClick={this.dataId ? this.handleUpdate : this.handleClick}
              disabled={loading}
              className={buttonClassname}
            />
            {loading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={
                  this.dataId
                    ? classes.buttonProgressUpdate
                    : classes.buttonProgressSave
                }
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error={questionsError}
              labelName={translate('QuestionText')}
              inputType="text"
              value={this.state.questionsModel.text}
              name="text"
              onChange={this.handleChange}
            />
            <Input
              error={questionsError}
              labelName={translate('Duration')}
              inputType="number"
              value={this.state.questionsModel.duration}
              name="duration"
              onChange={this.handleChange}
            />
            <DropDown
              error={questionsError}
              data={questionDifficulty}
              name="difficultyId"
              labelName={translate('QuestionDifficulty')}
              onChange={this.handleChange}
              value={this.state.questionsModel.difficultyId}
            />
            <DropDown
              error={questionsError}
              data={questionCategory}
              name="categoryId"
              labelName={translate('Category')}
              onChange={this.handleChange}
              value={this.state.questionsModel.categoryId}
              describable
            >
              <QuestionCategory />
            </DropDown>
          </Grid>
          <Grid item xs={12} sm={12} />
          <Header pageHeader={translate('Options')} />
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Options
            questionId={this.dataId ? this.dataId : questionId}
            basicOptionModel={model => {
              if (model) {
                this.setState({
                  newOptions: model
                });
              }
            }}
          />
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateQuestionLoading,
    createQuestion,
    errorCreateQuestion
  } = state.createQuestion;

  let newQuestion = createQuestion;

  const {
    isOptionListByQuestionIdLoading,
    optionsListByQuestionId,
    errorOptionListByQuestionId
  } = state.optionListByQuestionId;

  const { isQuestionLoading, question, errorQuestion } = state.question;

  const {
    isUpdateQuestionLoading,
    updateQuestion,
    errorUpdateQuestion
  } = state.updateQuestion;

  const {
    isCreateOptionLoading,
    createOption,
    errorCreateOption
  } = state.createOption;

  const {
    isUpdateOptionLoading,
    updateOption,
    errorUpdateOption
  } = state.updateOption;

  const {
    isQuestionDifficultyListLoading,
    questionDifficulties,
    errorQuestionDifficultyList
  } = state.questionDifficultyList;

  const {
    isQuestionCategoryListLoading,
    questionCategories,
    errorQuestionCategoryList
  } = state.questionCategoryList;

  return {
    isCreateQuestionLoading,
    createQuestion,
    errorCreateQuestion,
    newQuestion,
    isOptionListByQuestionIdLoading,
    optionsListByQuestionId,
    errorOptionListByQuestionId,
    isQuestionLoading,
    question,
    errorQuestion,
    isUpdateQuestionLoading,
    updateQuestion,
    errorUpdateQuestion,
    isCreateOptionLoading,
    createOption,
    errorCreateOption,
    isUpdateOptionLoading,
    updateOption,
    errorUpdateOption,
    isQuestionDifficultyListLoading,
    questionDifficulties,
    errorQuestionDifficultyList,
    isQuestionCategoryListLoading,
    questionCategories,
    errorQuestionCategoryList
  };
};

const mapDispatchToProps = {
  createQuestion,
  fetchOptionsByQuestionId,
  fetchQuestion,
  updateQuestion,
  createOption,
  updateOption,
  fetchQuestionDifficultyList,
  fetchQuestionCategoryList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(NewQuestion));
