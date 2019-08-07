import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Card from '../../components/Card/CardComponents';
import Modal from '../../components/Modal/ModalComponent';


class Session extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            openDialog: false,
        }
    }

    handleClickOpenDialog = () => {
        this.setState({ openDialog: true });
    };

    handleCloseDialog = () => {
        this.setState({ openDialog: false });
    };

    render() {
        const { openDialog, classroomList } = this.state;
        const { classes, trainingId } = this.props;

        return (

            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Grid item xs={12} sm={3}>
                        <CardButton
                            cardName={translate('AddNewClassroom')}
                            cardDescription={translate('YouCanCreateDifferentTrainingsForEachTrainingSeries')}
                            onClick={this.handleClickOpenDialog}
                            iconName="gamepad"
                        />
                    </Grid>
                    <Grid item xs={12} sm={9}>
                        {/* <Card
                            data={classroomList}
                            url="trainingseries"
                        /> */}
                    </Grid>
                </Grid>
                <Modal
                    titleName={translate('AddNewClassroom')}
                    open={openDialog}
                    handleClose={this.handleCloseDialog}
                >
                </Modal>
            </div >
        );
    }
}


export default withStyles(Style)(Session);