import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider } from '@material-ui/core';
import Style from '../Style';
import Header from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Table from 'material-table';

import { connect } from 'react-redux';
import { fetchQuestionList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Question';

class Questions extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      questionsColumns: [
        { title: translate('QuestionText'), field: 'text' },
        { title: translate('Duration'), field: 'duration' },
        { title: 'Soru Tipi**', field: 'createdAt' },
        { title: 'Kategori**', field: 'createdBy' }
      ],
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
      });
    }
    if (
      prevProps.isQuestionListLoading &&
      !isQuestionListLoading &&
      questionsList
    ) {
      questionsList.items.map(
        (question, id) => (
          (question.createdAt = 'Düz Metin'),
          (question.createdBy = 'Genel Kültür')
        )
      );
      this.setState({
        questions: questionsList.items
      });
    }
  }

  componentDidMount() {
    const { fetchQuestionList } = this.props;
    fetchQuestionList(0, 100);
  }

  handleClick = () => {
    this.props.history.push('/question');
    localStorage.removeItem('dataid');
  };

  handleChangeQuestion = id => {
    this.props.history.push(`/question/${id}`);
    localStorage.setItem('dataid', id);
  };

  render() {
    const { questions, questionsColumns } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={6} sm={11}>
            <Header pageHeader={translate('Questions')} />
          </Grid>
          <Grid item xs={6} sm={1}>
            <Button
              onClick={this.handleClick}
              buttonColor="primary"
              buttonName={translate('NewQuestion')}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Table
              title={translate('Questions')}
              columns={questionsColumns}
              data={questions}
              actions={[
                {
                  icon: 'edit',
                  onClick: (event, rowData) =>
                    this.handleChangeQuestion(rowData.id)
                }
              ]}
              options={{
                actionsColumnIndex: -1
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
    isQuestionListLoading,
    questionsList,
    errorQuestionList
  } = state.questionList;

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
