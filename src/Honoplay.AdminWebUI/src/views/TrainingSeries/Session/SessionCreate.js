import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress, Snackbar } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';
import MySnackbarContentWrapper from "../../../components/Snackbar/SnackbarContextComponent";

import { connect } from "react-redux";
import { createSession, fetchSessionListByClassroomId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session";

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
            open: false,
            sessionLoading: false,
            sessionError: false
        }
    }

    classroomId = null;

    componentDidUpdate(prevProps) {
        const {
            isCreateSessionLoading,
            newSession,
            errorCreateSession
        } = this.props;

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
            this.props.fetchSessionListByClassroomId(this.classroomId);
            if (!errorCreateSession) {
                this.setState({
                    sessionLoading: false,
                    sessionError: false,
                });
            }
        }
    }

    handleClick = () => {
        this.props.createSession(this.state.session);
    }

    handleClose = (reason) => {
        if (reason === "clickaway") {
            return;
        }
        this.setState({ open: false });
    };

    render() {
        const { sessionLoading, game, session, classroomListError, open, sessionError } = this.state;
        const { classes, classroomId } = this.props;

        this.state.session.createSessionModels.map((session) => {
            session.classroomId = classroomId;
        })

        this.classroomId = classroomId;

        console.log('claasss', this.classroomId);

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    {session.createSessionModels.map((session, id) => (
                        <Grid item xs={12} sm={12} key={id}>
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


    return {
        isCreateSessionLoading,
        newSession,
        errorCreateSession,
    };
};

const mapDispatchToProps = {
    createSession,
    fetchSessionListByClassroomId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(SessionCreate));