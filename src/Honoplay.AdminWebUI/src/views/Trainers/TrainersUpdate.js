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
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { updateTrainer, fetchTrainer, fetchTrainersList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Trainer";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";

class TraineesUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            departments: [],
            trainerModel: {
                name: '',
                surname: '',
                email: '',
                phoneNumber: '',
                departmentId: '',
                professionId: ''
            },
            loadingTrainer: false,
            loadingUpdate: false,
            success: false,
            updateError: false
        }
    }

    dataId = localStorage.getItem("dataid");

    componentDidUpdate(prevProps) {
        const {
            isDepartmentListLoading,
            departmentList,
            isUpdateTrainerLoading,
            errorUpdateTrainer,
            updateTrainer,
            isTrainerLoading,
            errorTrainer,
            trainer,
        } = this.props;

        if (prevProps.isTrainerLoading && !isTrainerLoading) {
            console.log('geliyoor');
            this.setState({
                loadingTrainer: true
            })
        }
        if (prevProps.isTrainerLoading && !isTrainerLoading && trainer) {
            console.log('buradayım');
            console.log('beni mi istedin',trainer.items);
            if (!errorTrainer) {
                this.setState({
                    trainerModel: trainer.items[0],
                })
            }
        }
        if (prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList) {
            this.setState({
                departments: departmentList.items
            })
        }
        if (prevProps.isUpdateTrainerLoading && !isUpdateTrainerLoading) {
            console.log('burda');
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateTrainer && errorUpdateTrainer) {
            console.log('burda2');
            console.log(errorUpdateTrainer);
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateTrainerLoading && !isUpdateTrainerLoading && updateTrainer) {
            console.log('burda3');
            if (!errorUpdateTrainer) {
                console.log('burda4');
                this.props.fetchTrainersList(0, 50);
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
        this.props.fetchDepartmentList(0, 50);
        this.props.fetchTrainer(parseInt(this.dataId));
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            trainerModel: {
                ...prevState.trainerModel,
                [name]: value
            },
            updateError: false
        }))
    }

    handleClick = () => {
        this.props.updateTrainer(this.state.trainerModel);
        console.log(this.state.trainerModel);
    }

    render() {
        const { classes } = this.props;
        const {
            loadingTrainer,
            departments,
            success,
            loadingUpdate,
            updateError,
            trainerModel
        } = this.state;
        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });
        console.log(this.dataId);
        return (

            <div className={classes.root}>
                {loadingTrainer == false ?
                    <CircularProgress
                        size={50}
                        disableShrink={true}
                        className={classes.progressModal}
                    /> :
                    <Grid container spacing={24}>
                        <Grid item xs={12} sm={12}>
                            <Input
                                error={updateError}
                                labelName={translate('Name')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="name"
                                value={trainerModel.name}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('Surname')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="surname"
                                value={trainerModel.surname}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('EmailAddress')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="email"
                                value={trainerModel.email}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('PhoneNumber')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="phoneNumber"
                                value={trainerModel.phoneNumber}
                            />
                            <DropDown
                                error={updateError}
                                data={departments}
                                labelName={translate('Department')}
                                onChange={this.handleChange}
                                name="departmentId"
                                value={trainerModel.departmentId}
                            />
                            <Input
                                error={updateError}
                                labelName={translate('TrainerExpertise')}
                                inputType="text"
                                onChange={this.handleChange}
                                name="professionId"
                                value={trainerModel.professionId}
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
        isTrainerLoading,
        errorTrainer,
        trainer
    } = state.trainer;

    console.log('dırım dırım dırım dırmdım',trainer);

    const {
        isTrainerListLoading,
        errorTrainerList,
        trainersList
    } = state.trainersList;

    const {
        errorDepartmentList,
        isDepartmentListLoading,
        departmentList
    } = state.departmentList;

    const {
        isUpdateTrainerLoading,
        errorUpdateTrainer,
        updateTrainer
    } = state.updateTrainer;

    return {
        isTrainerLoading,
        errorTrainer,
        trainer,
        isTrainerListLoading,
        errorTrainerList,
        trainersList,
        errorDepartmentList,
        isDepartmentListLoading,
        departmentList,
        isUpdateTrainerLoading,
        errorUpdateTrainer,
        updateTrainer
    };
};

const mapDispatchToProps = {
    fetchDepartmentList,
    updateTrainer,
    fetchTrainer,
    fetchTrainersList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TraineesUpdate));
