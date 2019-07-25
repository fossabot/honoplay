import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import classNames from "classnames";
import {
    Grid,
    CircularProgress
} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { updateQuestion, fetchQuestion, fetchQuestionList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Question";


class QuestionsUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            questionModel: {
                text: '',
                duration: ''
            },
            loadingTrainee: false,
            loadingUpdate: false,
            success: false,
            updateError: false
        }
    }

    dataId = localStorage.getItem("dataid");

    componentDidUpdate(prevProps) {
        const {
            isUpdateQuestionLoading,
            updateQuestion,
            errorUpdateQuestion,
            isQuestionLoading,
            question,
            errorQuestion
        } = this.props;

        if (prevProps.isQuestionLoading && !isQuestionLoading) {
            this.setState({
                loadingTrainee: true
            })
        }
        if (prevProps.isQuestionLoading && !isQuestionLoading && question) {
            if (!errorQuestion) {
                this.setState({
                    questionModel: question.items[0],
                })
            }
        }

        if (!prevProps.isUpdateQuestionLoading && isUpdateQuestionLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateQuestion && errorUpdateQuestion) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateQuestionLoading && !isUpdateQuestionLoading && updateQuestion) {
            if (!errorUpdateQuestion) {
                this.props.fetchQuestionList(0, 50);
                this.setState({
                    updateError: false,
                    loadingUpdate: false,
                    success: true,
                });
                setTimeout(() => {
                    this.setState({ success: false });
                }, 1000);
            }
        }
    }

    componentDidMount() {
        this.props.fetchQuestion(parseInt(this.dataId));
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            questionModel: {
                ...prevState.questionModel,
                [name]: value
            },
            updateError: false
        }))
    }

    handleClick = () => {
        this.props.updateQuestion(this.state.questionModel);
    }

    render() {
        const { classes } = this.props;
        const {
            loadingTrainee,
            questionModel,
            success,
            loadingUpdate,
            updateError
        } = this.state;
        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });
        return (

            <div className={classes.root}>
                {loadingTrainee == false ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={24}>
                        <Grid item xs={12} sm={12}>
                            <Input
                                error={updateError}
                                labelName={translate('QuestionText')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="text"
                                value={questionModel.text}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('Duration')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="duration"
                                value={questionModel.duration}
                            />
                        </Grid>
                        <Grid item xs={12} sm={12} />
                        <Grid item xs={12} sm={11} />
                        <Grid item xs={12} sm={1}>
                            <Button
                                className={buttonClassname}
                                buttonColor="primary"
                                buttonName={translate('Update')}
                                onClick={this.handleClick}
                                disabled={loadingUpdate}
                            />
                            {loadingUpdate && (
                                <CircularProgress
                                    size={24}
                                    disableShrink={true}
                                    className={classes.buttonProgressUpdate}
                                />
                            )}
                        </Grid>
                    </Grid>
                }
            </div>

        );
    }
}

const mapStateToProps = state => {

    const {
        isUpdateQuestionLoading,
        updateQuestion,
        errorUpdateQuestion
    } = state.updateQuestion;

    const {
        isQuestionListLoading,
        questionsList,
        errorQuestionList
    } = state.questionList;

    const {
        isQuestionLoading,
        question,
        errorQuestion
    } = state.question;

    return {
        isUpdateQuestionLoading,
        updateQuestion,
        errorUpdateQuestion,
        isQuestionListLoading,
        questionsList,
        errorQuestionList,
        isQuestionLoading,
        question,
        errorQuestion
    };
};

const mapDispatchToProps = {
    updateQuestion,
    fetchQuestion,
    fetchQuestionList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(QuestionsUpdate));
