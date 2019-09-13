import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Options from './Options';
import ExpansionPanel from '../../components/ExpansionPanel/ControlledExpansionPanels ';
import Table from '../../components/Table/TableComponent';
import { booleanToString } from '../../helpers/Converter';

import { connect } from "react-redux";
import { createQuestion } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Question";
import { fetchOptionsByQuestionId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Options";

class NewQuestion extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      questionsError: false,
      question: '',
      questionId: null,
      optionsColumns: [
        { title: translate('Order'), field: "visibilityOrder" },
        { title: translate('Options'), field: "text" },
        { title: translate('Answer'), field: "isCorrect" }
      ],
      options: []
    }
  }

 

  componentDidUpdate(prevProps) {
    const {
      isCreateQuestionLoading,
      errorCreateQuestion,
      newQuestion,
      isOptionListByQuestionIdLoading,
      optionsListByQuestionId,
      errorOptionListByQuestionId
    } = this.props;


    if (!prevProps.isCreateQuestionLoading && isCreateQuestionLoading) {
      this.setState({
        loading: true
      })
    }
    if (!prevProps.errorCreateQuestion && errorCreateQuestion) {
      this.setState({
        questionsError: true,
        loading: false
      })
    }
    if (prevProps.isCreateQuestionLoading && !isCreateQuestionLoading && newQuestion) {
      if (!errorCreateQuestion) {
        this.setState({
          question: newQuestion.items[0].text,
          questionId: newQuestion.items[0].id,
          loading: false,
          questionsError: false,
        });

      }
    }
    if (prevProps.isOptionListByQuestionIdLoading && !isOptionListByQuestionIdLoading && optionsListByQuestionId) {
      booleanToString(optionsListByQuestionId.items);
      this.setState({
        options: optionsListByQuestionId.items
      })
    }
  }

  questionsModel = {
    text: '',
    duration: ''
  }

  componentDidMount() {
    this.props.fetchOptionsByQuestionId(this.state.questionId);
  }

  handleClick = () => {
    this.props.createQuestion(this.questionsModel);
  }

  render() {
    const { questionsError, loading, question, questionId, optionsColumns, options } = this.state;
    const { classes } = this.props;

    return (

      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={translate('CreateAQuestion')}
            />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error={questionsError}
              labelName={translate('QuestionText')}
              inputType="text"
              onChange={e => {
                this.questionsModel.text = e.target.value;
                this.setState({ questionsError: false });
              }}
            />
            <Input
              error={questionsError}
              labelName={translate('Duration')}
              inputType="text"
              onChange={e => {
                this.questionsModel.duration = e.target.value;
                this.setState({ questionsError: false });
              }}
            />
          </Grid>
          <Grid item xs={12} sm={11} />
          <Grid item xs={12} sm={1} >
            <Button
              buttonColor="secondary"
              buttonName={translate('Save')}
              onClick={this.handleClick}
              disabled={loading}
            />
            {loading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgressSave}
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={translate('Options')}
            />
          </Grid>
          <Grid item xs={12} sm={12}> <Divider /> </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <ExpansionPanel open={question}
              panelDetails={question}
            >
              <Options questionId={questionId} />
            </ExpansionPanel>
            <Grid item xs={12} sm={12}>
              <Table
                columns={optionsColumns}
                data={options}
                isSelected={selected => { }}
                remove
              >
              </Table>
            </Grid>
          </Grid>
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

  return {
    isCreateQuestionLoading,
    createQuestion,
    errorCreateQuestion,
    newQuestion,
    isOptionListByQuestionIdLoading,
    optionsListByQuestionId,
    errorOptionListByQuestionId
  };
};

const mapDispatchToProps = {
  createQuestion,
  fetchOptionsByQuestionId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(NewQuestion));