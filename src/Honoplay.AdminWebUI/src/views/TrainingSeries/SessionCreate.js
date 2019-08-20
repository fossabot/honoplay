import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress, Snackbar } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';
import MySnackbarContentWrapper from "../../components/Snackbar/SnackbarContextComponent";

import { connect } from "react-redux";
import { createSession, fetchSessionList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session";
import { fetchClassroomList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom";

class SessionCreate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            sessionLoading: false,
            game: [
                { id: 1, name: 'Kelime Oyunu', },
                { id: 2, name: 'Puzzle', }
            ],
            session: {
                createSessionModels: [
                    {
                        gameId: '',
                        classroomId: '',
                        name: '',
                    }
                ]
            },
            classroomList: [],
            open: false,
            sessionLoading: false,
            sessionError: false
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isClassroomListLoading,
            classroomsList,
            errorClassroomList,
            isCreateSessionLoading,
            newSession,
            errorCreateSession
        } = this.props;

        if (!prevProps.errorClassroomList && errorClassroomList) {
            this.setState({
                classroomListError: true,
                open: true
            })
        }
        if (prevProps.isClassroomListLoading && !isClassroomListLoading && classroomsList) {
            this.setState({
                classroomList: classroomsList.items,
                open: false,
            })
        }
        if (!prevProps.isCreateSessionLoading && isCreateSessionLoading) {
            this.setState({
                sessionLoading: true
            })
        }
        if (!prevProps.errorCreateSession && errorCreateSession) {
            this.setState({
                sessionError: true,
                sessionLoading: false
            })
        }
        if (prevProps.isCreateSessionLoading && !isCreateSessionLoading && newSession) {
           this.props.fetchSessionList(0,50);
            if (!errorCreateSession) {
                this.setState({
                    sessionLoading: false,
                    sessionError: false,
                });
            }
        }
    }

    componentDidMount() {
        this.props.fetchClassroomList(0, 50);
    }

    handleClick = () => {
        console.log(this.state.session);
        this.props.createSession(this.state.session);
    }

    handleClose = (reason) => {
        if (reason === "clickaway") {
            return;
        }
        this.setState({ open: false });
    };

    render() {
        const { sessionLoading, game, session, classroomList, classroomListError, open, sessionError } = this.state;
        const { classes } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    {session.createSessionModels.map((session, id) => (
                        <Grid item xs={12} sm={12} key={id}>
                            <DropDown
                                error={sessionError}
                                data={classroomList}
                                labelName={translate('Classroom')}
                                onChange={e => {
                                    session.classroomId = e.target.value;
                                }}
                            />
                            <Input
                                error={sessionError}
                                labelName={translate('SessionName')}
                                inputType="text"
                                onChange={e => {
                                    session.name = e.target.value;
                                }}
                            />
                            <DropDown
                                error={sessionError}
                                data={game}
                                labelName={translate('Game')}
                                onChange={e => {
                                    session.gameId = e.target.value;
                                }}
                            />
                        </Grid>
                    ))}
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            onClick={this.handleClick}
                            disabled={sessionLoading}
                        />
                        {sessionLoading && (
                            <CircularProgress
                                size={24}
                                disableShrink={true}
                                className={classes.buttonProgressSave}
                            />
                        )}
                    </Grid>
                </Grid>
                <Snackbar
                    anchorOrigin={{
                        vertical: "bottom",
                        horizontal: "left"
                    }}
                    autoHideDuration={7000}
                    open={open}
                    onClose={this.handleClose}
                >
                    <MySnackbarContentWrapper
                        onClose={this.handleClose}
                        variant={classroomListError && "error"}
                        message={
                            classroomListError
                            && translate('YouMustAddAClassBeforeCreatingASession')
                        }
                    />
                </Snackbar>
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isCreateSessionLoading,
        createSession,
        errorCreateSession
    } = state.createSession;

    let newSession = createSession;

    console.log('session:', state.createSession);

    const {
        isSessionListLoading,
        sessionList,
        errorSessionList
    } = state.sessionList;

    const {
        isClassroomListLoading,
        classroomsList,
        errorClassroomList
    } = state.classroomList;

    return {
        isCreateSessionLoading,
        newSession,
        errorCreateSession,
        isSessionListLoading,
        sessionList,
        errorSessionList,
        isClassroomListLoading,
        classroomsList,
        errorClassroomList
    };
};

const mapDispatchToProps = {
    createSession,
    fetchSessionList,
    fetchClassroomList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(SessionCreate));