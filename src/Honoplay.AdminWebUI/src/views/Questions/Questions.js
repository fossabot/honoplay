import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Table from '../../components/Table/TableComponent';
import QuestionsUpdate from './QuestionsUpdate';

import { connect } from "react-redux";
import { fetchQuestionList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Question";

class Questions extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      questionsColumns: [
        { title: "Soru Metni", field: "text" },
        { title: "Saniye", field: "duration" }],
      questions: []
    };
  }

  componentDidUpdate(prevProps) {
    const {
      isQuestionListLoading,
      questionsList,
      errorQuestionList
    } = this.props;

    if (!prevProps.errorQuestionList && errorQuestionList) {
      this.setState({
        departmentListError: true
      })
    }
    if (prevProps.isQuestionListLoading && !isQuestionListLoading && questionsList) {
      this.setState({
        questions: questionsList.items
      })
    }
  }

  componentDidMount() {
    const { fetchQuestionList } = this.props;
    fetchQuestionList(0, 50);
  }

  handleClick = () => {
    this.props.history.push("/home/addquestion");
  }

  render() {
    const { questions, questionsColumns } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={6} sm={11}>
            <Typography
              pageHeader={translate('Questions')}
            />
          </Grid>
          <Grid item xs={6} sm={1}>
            <Button
              onClick={this.handleClick}
              buttonColor="secondary"
              buttonIcon="plus"
              buttonName={translate('NewQuestion')}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Table
              columns={questionsColumns}
              data={questions}
            >
              <QuestionsUpdate/>
            </Table>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {

  const {
    isQuestionListLoading,
    questionsList,
    errorQuestionList
  } = state.questionList

  return {
    isQuestionListLoading,
    questionsList,
    errorQuestionList
  };
};

const mapDispatchToProps = {
  fetchQuestionList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Questions));