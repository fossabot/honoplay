import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import {
  Grid,
  Checkbox,
  MuiThemeProvider,
  Divider,
  Typography
} from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from 'react-redux';
import { fetchOptionsByQuestionId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Options';

class Options extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: false,
      optionError: false,
      order: 2,
      options: [
        {
          questionId: '',
          text: '',
          visibilityOrder: 1,
          isCorrect: false
        }
      ]
    };
  }

  questionId = null;

  optionsModel = {
    createOptionModels: []
  };

  componentDidUpdate(prevProps) {
    const {
      isOptionListByQuestionIdLoading,
      optionsListByQuestionId
    } = this.props;

    if (
      prevProps.isOptionListByQuestionIdLoading &&
      !isOptionListByQuestionIdLoading &&
      optionsListByQuestionId
    ) {
      this.setState({
        options: optionsListByQuestionId.items,
        order: optionsListByQuestionId.items.length + 1
      });
      this.props.basicOptionModel(this.optionsModel);
    }
  }

  componentDidMount() {
    this.props.fetchOptionsByQuestionId(this.questionId);
  }

  optionAdd = () => {
    this.setState({
      order: this.state.order + 1,
      options: this.state.options.concat([
        {
          questionId: '',
          text: '',
          visibilityOrder: this.state.order,
          isCorrect: false
        }
      ])
    });
  };

  handleChangeOption = id => evt => {
    const newOptions = this.state.options.map((option, optionId) => {
      if (id !== optionId) return option;
      return { ...option, text: evt.target.value };
    });
    this.setState({ options: newOptions });

    this.props.basicOptionModel(this.optionsModel);
  };

  optionRemove = id => () => {
    this.setState({
      order: this.state.order - 1,
      options: this.state.options.filter((o, oid) => id !== oid)
    });
  };

  render() {
    const { classes, questionId } = this.props;

    this.questionId = questionId;

    this.state.options.map((option, id) => {
      option.questionId = this.questionId;
    });

    this.optionsModel.createOptionModels = this.state.options;

    return (
      <MuiThemeProvider theme={theme}>
        <div className={classes.root}>
          <Grid container spacing={3}>
            <Grid item xs={12} sm={12} />
            <Grid item xs={12} sm={12}>
              <Typography>{translate('CorrectAnswer')}</Typography>
            </Grid>
            {this.state.options.map((option, id) => (
              <Grid container direction="row" spacing={3} key={id}>
                <Grid item xs={12} sm={1}>
                  <Checkbox
                    checked={Boolean(option.isCorrect)}
                    color="secondary"
                    value={option.text}
                    onChange={e => {
                      option.isCorrect = true;
                      this.props.basicOptionModel(this.optionsModel);
                    }}
                    className={classes.checkbox}
                  />
                </Grid>
                <Grid item xs={12} sm={1}>
                  <Input
                    inputType="text"
                    placeholder={translate('VisibilityOrder')}
                    value={option.visibilityOrder}
                  />
                </Grid>
                <Grid item xs={12} sm={2}>
                  <Input
                    inputType="text"
                    placeholder={translate('Option')}
                    onChange={this.handleChangeOption(id)}
                    value={option.text}
                  />
                </Grid>
                {option.id ? (
                  ''
                ) : (
                  <Grid item xs={12} sm={1}>
                    <Button
                      buttonColor="secondary"
                      onClick={this.optionRemove(id)}
                      buttonIcon="minus"
                    />
                  </Grid>
                )}
                <Grid item xs={12} sm={7}></Grid>
                <Grid item xs={12} sm={12}></Grid>
              </Grid>
            ))}
            <Grid item xs={12} sm={12} />
            <Grid item xs={12} sm={12}>
              <Button
                buttonColor="secondary"
                buttonName={translate('AddOption')}
                onClick={this.optionAdd}
                buttonIcon="plus"
              />
            </Grid>
            <Grid item xs={12} sm={12}>
              <Divider />
            </Grid>
          </Grid>
        </div>
      </MuiThemeProvider>
    );
  }
}

const mapStateToProps = state => {
  const {
    isOptionListByQuestionIdLoading,
    optionsListByQuestionId,
    errorOptionListByQuestionId
  } = state.optionListByQuestionId;

  return {
    isOptionListByQuestionIdLoading,
    optionsListByQuestionId,
    errorOptionListByQuestionId
  };
};

const mapDispatchToProps = {
  fetchOptionsByQuestionId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Options));
