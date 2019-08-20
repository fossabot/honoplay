import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Card from '../../components/Card/CardComponents';
import Modal from '../../components/Modal/ModalComponent';
import SessionCreate from './SessionCreate';

import { connect } from "react-redux";
import { fetchSessionList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session";

class Session extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            openDialog: false,
            sessionListError: false,
            sessionList: []
        }
    }

    componentDidUpdate(prevProps) {
        const {
            isSessionListLoading,
            sessionList,
            errorSessionList
        } = this.props;

        if (!prevProps.errorSessionList && errorSessionList) {
            this.setState({
                sessionListError: true
            })
        }
        if (prevProps.isSessionListLoading && !isSessionListLoading && sessionList) {
            this.setState({
                sessionList: sessionList.items
            })
        }
    }

    componentDidMount() {
        this.props.fetchSessionList(0,50);
    }

    handleClickOpenDialog = () => {
        this.setState({ openDialog: true });
    };

    handleCloseDialog = () => {
        this.setState({ openDialog: false });
    };

    render() {
        const { openDialog, sessionList } = this.state;
        const { classes, classroomId } = this.props;

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
                            url="trainingseries"
                        />
                    </Grid>
                </Grid>
                <Modal
                    titleName={translate('AddNewSession')}
                    open={openDialog}
                    handleClose={this.handleCloseDialog}
                >
                    <SessionCreate classroomId={classroomId} />
                </Modal>
            </div >
        );
    }
}


const mapStateToProps = state => {

    const {
        isSessionListLoading,
        sessionList,
        errorSessionList
    } = state.sessionList;

    return {
        isSessionListLoading,
        sessionList,
        errorSessionList
    };
};

const mapDispatchToProps = {
    fetchSessionList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Session));