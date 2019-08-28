import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import classNames from "classnames";
import {
    Grid,
    CircularProgress,
    TextField,
    InputAdornment,
    IconButton,
    InputLabel
} from '@material-ui/core';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from "react-redux";
import { updateTrainer, fetchTrainer, fetchTrainersList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department";
import { fetchProfessionList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Profession";

class TraineesUpdate extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            professions: [],
            departments: [],
            trainerModel: {
                name: '',
                surname: '',
                email: '',
                phoneNumber: '',
                departmentId: '',
                professionId: '',
                password: ''
            },
            loadingTrainer: false,
            loadingUpdate: false,
            success: false,
            updateError: false,
            confirmPassword: null
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
            isProfessionListLoading,
            professionList,
        } = this.props;

        if (prevProps.isProfessionListLoading && !isProfessionListLoading && professionList) {
            this.setState({
                professions: professionList.items
            })
        }

        if (prevProps.isTrainerLoading && !isTrainerLoading) {
            this.setState({
                loadingTrainer: true
            })
        }
        if (prevProps.isTrainerLoading && !isTrainerLoading && trainer) {
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
        if (!prevProps.isUpdateTrainerLoading && isUpdateTrainerLoading) {
            this.setState({
                loadingUpdate: true
            })
        }
        if (!prevProps.errorUpdateTrainer && errorUpdateTrainer) {
            this.setState({
                updateError: true,
                loadingUpdate: false,
                success: false
            })
        }
        if (prevProps.isUpdateTrainerLoading && !isUpdateTrainerLoading && updateTrainer) {
            if (!errorUpdateTrainer) {
                if (this.state.confirmPassword == this.state.trainerModel.password) {
                    this.props.fetchTrainersList(0, 50);
                    this.setState({
                        updateError: false,
                        loadingUpdate: false,
                        success: true,
                    });
                    setTimeout(() => {
                        this.setState({ success: false });
                    }, 1000);
                } else {
                    this.setState({
                        updateError: true,
                        success: false,
                        loadingUpdate: false,
                    });
                }
            }
        }
    }

    componentDidMount() {
        this.props.fetchDepartmentList(0, 50);
        this.props.fetchTrainer(parseInt(this.dataId));
        this.props.fetchProfessionList(0, 50);
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            trainerModel: {
                ...prevState.trainerModel,
                [name]: value
            },
            updateError: false,
            [name]: value
        }))
    }

    handleClick = () => {
        this.props.updateTrainer(this.state.trainerModel);
    }

    handleClickShowPassword = () => {
        this.setState(
            state => ({
                showPassword: !state.showPassword
            }));
    };

    render() {
        const { classes } = this.props;
        const {
            loadingTrainer,
            departments,
            success,
            loadingUpdate,
            updateError,
            trainerModel,
            professions
        } = this.state;
        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });

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
                            <DropDown
                                error={updateError}
                                onChange={this.handleChange}
                                data={professions}
                                labelName={translate('TrainerExpertise')}
                                name="professionId"
                                value={trainerModel.professionId}
                            />
                        </Grid>
                        <Grid item xs={12} sm={3}>
                            <InputLabel className={classes.bootstrapFormLabel}>
                                {translate('Password')}
                            </InputLabel>
                        </Grid>
                        <Grid item xs={12} sm={9}>
                            <TextField
                                error={updateError && true}
                                className={classes.passwordInput}
                                name="password"
                                type={this.state.showPassword ? 'text' : 'password'}
                                onChange={this.handleChange}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton
                                                onClick={this.handleClickShowPassword}
                                            >
                                                {this.state.showPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        </Grid>
                        <Grid item xs={12} sm={3}>
                            <InputLabel className={classes.bootstrapFormLabel}>
                                {translate('Confirm')}
                            </InputLabel>
                        </Grid>
                        <Grid item xs={12} sm={9}>
                            <TextField
                                error={updateError && true}
                                className={classes.passwordInput}
                                name="confirmPassword"
                                type={this.state.showPassword ? 'text' : 'password'}
                                onChange={this.handleChange}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton
                                                onClick={this.handleClickShowPassword}
                                            >
                                                {this.state.showPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
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

    const {
        isProfessionListLoading,
        professionList,
        errorProfessionList
    } = state.professionList;

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
        updateTrainer,
        isProfessionListLoading,
        professionList,
        errorProfessionList
    };
};

const mapDispatchToProps = {
    fetchDepartmentList,
    updateTrainer,
    fetchTrainer,
    fetchTrainersList,
    fetchProfessionList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(TraineesUpdate));
