import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../../Style';
import CardButton from '../../../components/Card/CardButton';
import Card from '../../../components/Card/CardComponents';
import Modal from '../../../components/Modal/ModalComponent';
import SessionCreate from './SessionCreate';
import SessionUpdate from './SessionUpdate';

import { connect } from "react-redux";
import { fetchSessionListByClassroomId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session";

class Session extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            openDialog: false,
            sessionListError: false,
            sessionList: [],
            sessionId: null
        }
    }

    classroomId = null;

    componentDidUpdate(prevProps) {
        const {
            isSessionListByClassroomIdLoading,
            sessionListByClassroomId,
            errorSessionListByClassroomId
        } = this.props;

        if (!prevProps.errorSessionListByClassroomId && errorSessionListByClassroomId) {
            this.setState({
                sessionListError: true
            })
        }
        if (prevProps.isSessionListByClassroomIdLoading && !isSessionListByClassroomIdLoading && sessionListByClassroomId) {
            this.setState({
                sessionList: sessionListByClassroomId.items
            })
        }
    }

    componentDidMount() {
        this.props.fetchSessionListByClassroomId(this.classroomId);
    }

    handleClickOpenDialog = () => {
        this.setState({ openDialog: true });
    };

    handleCloseDialog = () => {
        this.setState({ openDialog: false });
    };

    render() {
        const { openDialog, sessionList, sessionId } = this.state;
        const { classes, classroomId } = this.props;

        this.classroomId = classroomId;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={3}>
                        <CardButton
                            cardName={translate('AddNewSession')}
                            cardDescription={translate('YouCanCreateDifferentTrainingsForEachTrainingSeries')}
                            onClick={this.handleClickOpenDialog}
                            iconName="gamepad"
                        />
                    </Grid>
                    <Grid item xs={12} sm={9}>
                        <Card
                            data={sessionList}
                            titleName={translate('UpdateSession')}
                            id={id => {
                                if (id) {
                                    console.log(id);
                                    this.setState({
                                        sessionId: id
                                    });
                                }
                            }}
                        >
                            <SessionUpdate sessionId={sessionId} />
                        </Card>
                    </Grid>
                </Grid>
                <Modal
                    titleName={translate('AddNewSession')}
                    open={openDialog}
                    handleClose={this.handleCloseDialog}
                >
                    <SessionCreate classroomId={this.classroomId} />
                </Modal>
            </div >
        );
    }
}


const mapStateToProps = state => {

    const {
        isSessionListByClassroomIdLoading,
        sessionListByClassroomId,
        errorSessionListByClassroomId
    } = state.sessionListByClassroomId;

    return {
        isSessionListByClassroomIdLoading,
        sessionListByClassroomId,
        errorSessionListByClassroomId
    };
};

const mapDispatchToProps = {
    fetchSessionListByClassroomId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Session));