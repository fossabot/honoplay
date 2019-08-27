import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../../Style';
import CardButton from '../../../components/Card/CardButton';
import Card from '../../../components/Card/CardComponents';
import Modal from '../../../components/Modal/ModalComponent';
import ClassroomCreate from './ClassroomCreate';

import { connect } from "react-redux";
import { fetchClassroomListByTrainingId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom";
import SessionCreate from '../Session/SessionCreate';


class Classroom extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            openDialog: false,
            classroomListError: false,
            classroomList: [],
            classroomId: null,
        }
    }

    trainingId = null;

    componentDidUpdate(prevProps) {
        const {
            isClassroomListByTrainingIdLoading,
            classroomsListByTrainingId,
            errorClassroomListByTrainingId
        } = this.props;

        if (!prevProps.errorClassroomListByTrainingId && errorClassroomListByTrainingId) {
            this.setState({
                classroomListError: true
            })
        }
        if (prevProps.isClassroomListByTrainingIdLoading && !isClassroomListByTrainingIdLoading && classroomsListByTrainingId) {
            this.setState({
                classroomList: classroomsListByTrainingId.items
            })
        }
    }

    handleClickOpenDialog = () => {
        this.setState({ openDialog: true });
    };

    handleCloseDialog = () => {
        this.setState({ openDialog: false });
    };

    componentDidMount() {
        this.props.fetchClassroomListByTrainingId(this.trainingId);
    }

    render() {
        const { openDialog, classroomList, classroomId } = this.state;
        const { classes, trainingId } = this.props;

        this.trainingId = trainingId;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={3}>
                        <CardButton
                            cardName={translate('AddNewClassroom')}
                            cardDescription={translate('YouCanCreateDifferentTrainingsForEachTrainingSeries')}
                            onClick={this.handleClickOpenDialog}
                            iconName="users"
                        />
                    </Grid>
                    <Grid item xs={12} sm={9}>
                        <Card
                            data={classroomList}
                            forClassroom
                            titleName={translate('AddNewSession')}
                            id={id => {
                                if (id) {
                                    this.setState({
                                        classroomId: id
                                    });
                                }
                            }}
                        >
                            <SessionCreate classroomId={classroomId} />
                        </Card>
                    </Grid>
                </Grid>
                <Modal
                    titleName={translate('AddNewClassroom')}
                    open={openDialog}
                    handleClose={this.handleCloseDialog}
                >
                    <ClassroomCreate
                        trainingId={this.trainingId}
                    />
                </Modal>
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isClassroomListByTrainingIdLoading,
        classroomsListByTrainingId,
        errorClassroomListByTrainingId
    } = state.classroomListByTrainingId;

    return {
        isClassroomListByTrainingIdLoading,
        classroomsListByTrainingId,
        errorClassroomListByTrainingId
    };
};

const mapDispatchToProps = {
    fetchClassroomListByTrainingId
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Classroom));