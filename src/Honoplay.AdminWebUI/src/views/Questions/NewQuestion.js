import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, TextField, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Options from '../../components/Options/OptionsComponent';

import { connect } from "react-redux";
import { createQuestion } from "@omegabigdata/honoplay-redux-helper/Src/actions/Question";

class NewQuestion extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      questionsError: false,
      question: '',
      answerData: [
        { order: 1, answer: "Tasarım Departmanı" },
        { order: 2, answer: "Yazılım Departmanı" },
        { order: 3, answer: "İnsan Kaynakları Departmanı" },
      ],
    }
  }

  componentDidUpdate(prevProps) {
    const {
      isCreateQuestionLoading,
      errorCreateQuestion,
      newQuestion
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
          loading: false,
          questionsError: false,
        });
      
      }
    }
  }

  questionsModel = {
    text: '',
    duration: ''
  }
  handleClick = () => {
    this.props.createQuestion(this.questionsModel);
  }

  render() {
    const { questionsError, loading, question } = this.state;
    const { classes } = this.props;
    console.log('aaa', this.state.question);
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
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
                className={classes.buttonProgress}
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}> <Divider /> </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <TextField
              fullWidth
              margin="normal"
              variant="outlined"
              InputProps={{
                readOnly: true,
              }}
              value={question}
              rowsMax="4"
            />
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
  return {
    isCreateQuestionLoading,
    createQuestion,
    errorCreateQuestion,
    newQuestion
  };
};

const mapDispatchToProps = {
  createQuestion
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(NewQuestion));