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
import ImageInput from '../../components/Input/ImageInputComponent';
import SelectDropdown from '../../components/Input/SelectDropdown';
import QuestionTag from './QuestionTag';

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
import { fetchQuestionTypeList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/QuestionType';
import { createContentFile } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/ContentFile';
import { fetchTagList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Tag';

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
      questionCategory: [],
      questionTypes: [],
      questionTags: []
    };
  }

  dataId = localStorage.getItem('dataid');

  index = null;

  optionsModel = {
    createOptionModels: []
  };

  contentFileModel = {
    createContentFileModels: [
      {
        name: '',
        contentType: '',
        data: ''
      }
    ]
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
      errorQuestionCategoryList,
      isQuestionTypeListLoading,
      questionTypes,
      errorQuestionTypeList,
      isCreateContentFileLoading,
      newContentFile,
      errorCreateContentFile,
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
    if (!prevProps.isCreateContentFileLoading && isCreateContentFileLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorCreateContentFile && errorCreateContentFile) {
      this.setState({
        questionsError: true,
        loading: false
      });
    }
    if (
      prevProps.isCreateContentFileLoading &&
      !isCreateContentFileLoading &&
      newContentFile
    ) {
      if (!errorCreateContentFile) {
        this.setState({
          loading: false,
          questionsError: false
        });
        this.state.questionsModel.contentFileId = newContentFile.items[0][0].id;
        this.props.createQuestion(this.state.questionsModel);
      }
    }

    if (
      prevProps.isQuestionTypeListLoading &&
      !isQuestionTypeListLoading &&
      questionTypes
    ) {
      if (!errorQuestionTypeList) {
        this.setState({
          questionTypes: questionTypes.items
        });
      }
    }
    if (
      prevProps.isQuestionCategoryListLoading &&
      !isQuestionCategoryListLoading &&
      questionCategories
    ) {
      if (!errorQuestionCategoryList) {
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
    this.props.fetchQuestionTypeList(0, 50);
    this.props.fetchTagList(0, 50);
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
    if (this.state.questionsModel.typeId == 1) {
      this.props.createContentFile(this.contentFileModel);
    } else {
      this.props.createQuestion(this.state.questionsModel);
    }
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
      questionCategory,
      questionTypes,
      questionTags
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
            <DropDown
              error={questionsError}
              data={questionTypes}
              name="typeId"
              labelName={translate('QuestionType')}
              onChange={this.handleChange}
              value={this.state.questionsModel.typeId}
            />
            {this.state.questionsModel.typeId == 1 &&
              this.contentFileModel.createContentFileModels.map(
                (contentFile, index) => (
                  <ImageInput
                    key={index}
                    error={questionsError}
                    selectedImage={value => {
                      contentFile.data = value;
                    }}
                    name={value => {
                      contentFile.name = value;
                    }}
                    type={value => {
                      contentFile.contentType = value;
                    }}
                    labelName={translate('QuestionImage')}
                  />
                )
              )}
            <SelectDropdown
              describable
              options={questionTags}
              labelName={translate('QuestionTag')}
              selectedOption={value => {
                console.log(value);
              }}
            >
              <QuestionTag />
            </SelectDropdown>
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

  const {
    isQuestionTypeListLoading,
    questionTypes,
    errorQuestionTypeList
  } = state.questionTypeList;

  const {
    isCreateContentFileLoading,
    newContentFile,
    errorCreateContentFile
  } = state.createContentFile;

  const { isTagListLoading, tags, errorTagList } = state.tagList;

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
    errorQuestionCategoryList,
    isQuestionTypeListLoading,
    questionTypes,
    errorQuestionTypeList,
    isCreateContentFileLoading,
    newContentFile,
    errorCreateContentFile,
    isTagListLoading,
    tags,
    errorTagList
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
  fetchQuestionCategoryList,
  fetchQuestionTypeList,
  createContentFile,
  fetchTagList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(NewQuestion));
