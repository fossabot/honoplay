import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Card from '../../components/Card/CardComponents';
import Modal from '../../components/Modal/ModalComponent';
import ClassroomCreate from './ClassroomCreate';
import ClassroomUpdate from './ClassroomUpdate';

import { connect } from "react-redux";
import { fetchClassroomList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom";


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

    componentDidUpdate(prevProps) {
        const {
            isClassroomListLoading,
            classroomsList,
            errorClassroomList
        } = this.props;

        if (!prevProps.errorClassroomList && errorClassroomList) {
            this.setState({
                classroomListError: true
            })
        }
        if (prevProps.isClassroomListLoading && !isClassroomListLoading && classroomsList) {
            this.setState({
                classroomList: classroomsList.items
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
        this.props.fetchClassroomList(0, 50);
    }

    render() {
        const { openDialog, classroomList, classroomId } = this.state;
        const { classes, trainingId } = this.props;
        
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
                            titleName={translate('Classroom')}
                            id={id => {
                                if (id) {
                                    this.setState({
                                        classroomId: id
                                    });
                                }
                            }}
                        >
                            <ClassroomUpdate classroomId={classroomId}/>
                        </Card>
                    </Grid>
                </Grid>
                <Modal
                    titleName={translate('AddNewClassroom')}
                    open={openDialog}
                    handleClose={this.handleCloseDialog}
                >
                    <ClassroomCreate trainingId={trainingId} />
                </Modal>
            </div >
        );
    }
}

const mapStateToProps = state => {

    const {
        isClassroomListLoading,
        classroomsList,
        errorClassroomList
    } = state.classroomList;

    return {
        isClassroomListLoading,
        classroomsList,
        errorClassroomList
    };
};

const mapDispatchToProps = {
    fetchClassroomList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Classroom));