import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from "classnames";
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { fetchSession, updateSession, fetchSessionList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session';

class SessionUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            classrooms: null,
            session: {
                gameId: '',
                classroomId: '',
                name: '',
            },
            game: [
                { id: 1, name: 'Kelime Oyunu', },
                { id: 2, name: 'Puzzle', }
            ],
            loadingSession: false,
            updateError: false,
            loadingUpdate: false,
            success: false
        };
    }

    sessionId = null;

    componentDidUpdate(prevProps) {
        const {
            isSessionLoading,
            session,
            errorSession,
            isUpdateSessionLoading,
            updateSession,
            errorUpdateSession
        } = this.props;

        if (prevProps.isSessionLoading && !isSessionLoading) {
            this.setState({
                loadingSession: true
            })
        }
        if (prevProps.isSessionLoading && !isSessionLoading && session) {
            if (!errorSession) {
                this.setState({
                    session: session.items[0]
                })
            }
        }

        if (!prevProps.isUpdateSessionLoading && isUpdateSessionLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateSession && errorUpdateSession) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateSessionLoading && !isUpdateSessionLoading && updateSession) {
            if (!errorUpdateSession) {
                this.props.fetchSessionList(0, 50);
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
        this.props.fetchSession(this.sessionId);
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            session: {
                ...prevState.session,
                [name]: value
            },
            updateError: false
        }))
    }

    handleClick = () => {
        this.props.updateSession(this.state.session);
    }

    render() {
        const { session, loadingSession, loadingUpdate, success, game } = this.state;
        const { classes, sessionId } = this.props;

        this.sessionId = sessionId;

        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });

        return (

            <div className={classes.root}>
                {loadingSession == false ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={24}>
                        <Grid item xs={12} sm={12} >
                            <DropDown
                                labelName={translate('Game')}
                                data={game}
                                value={session.gameId}
                                name="gameId"
                                onChange={this.handleChange}
                            />
                            <Input
                                labelName={translate('SessionName')}
                                inputType="text"
                                value={session.name}
                                name="name"
                                onChange={this.handleChange}
                            />
                        </Grid>
                        <Grid item xs={12} sm={11} />
                        <Grid item xs={12} sm={1}>
                            <Button
                                buttonColor="primary"
                                buttonName={translate('Update')}
                                onClick={this.handleClick}
                                disabled={loadingUpdate}
                                className={buttonClassname}
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
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isSessionLoading,
        session,
        errorSession
    } = state.session;

    const {
        isUpdateSessionLoading,
        updateSession,
        errorUpdateSession
    } = state.updateSession;

    const {
        isSessionListLoading,
        sessionList,
        errorSessionList
    } = state.sessionList;

    return {
        isSessionLoading,
        session,
        errorSession,
        isUpdateSessionLoading,
        updateSession,
        errorUpdateSession,
        isSessionListLoading,
        sessionList,
        errorSessionList
    };
};

const mapDispatchToProps = {
    fetchSession,
    updateSession,
    fetchSessionList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(SessionUpdate));