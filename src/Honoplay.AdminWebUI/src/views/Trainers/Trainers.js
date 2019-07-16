import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
    Grid,
    Divider,
    CircularProgress
} from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';
import Chip from '../../components/Chip/ChipComponent';

import { connect } from "react-redux";
import {
    createTrainer,
    fetchTrainersList
} from "@omegabigdata/honoplay-redux-helper/Src/actions/Trainer";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";

import { departmentToString } from "../../helpers/Converter";

class Trainers extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            departments: [],
            trainer: [],
            departmentListError: false,
            trainerError: false,
            trainerExpertiseData: [
                { id: 1, name: 'Web Tasarım', },
                { id: 2, name: 'Ticaret Hukuk', },
                { id: 3, name: 'Yazılım', },
                { id: 4, name: 'Siber Güvenlik', },
                { id: 5, name: 'İnsan Kaynakları', },
            ],
            trainerColumns: [
                { title: "Ad", field: "name" },
                { title: "Soyad", field: "surname" },
                { title: "TCKN", field: "email" },
                { title: "Cep Telefonu", field: "phoneNumber" },
                { title: "Departman", field: "departmentId" },
                { title: "Uzmanlık Alanları", field: "professionId" }
            ],
        };
    }

    trainerModel = {
        name: '',
        surname: '',
        email: '',
        phoneNumber: '',
        departmentId: '',
        professionId: ''
    };

    componentDidUpdate(prevProps) {
        const {
            departmentList,
            isDepartmentListLoading,
            errorDepartmentList,
            isCreateTrainerLoading,
            createTrainer,
            errorCreateTrainer,
            isTrainerListLoading,
            errorTrainerList,
            trainersList
        } = this.props;

        if (!prevProps.errorDepartmentList && errorDepartmentList) {
            this.setState({
                departmentListError: true
            })
        }
        if (prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList) {
            this.setState({
                departments: departmentList.items
            })
        }
        if (!prevProps.isCreateTrainerLoading && isCreateTrainerLoading) {
            this.setState({
                loading: true
            })
        }
        if (!prevProps.errorCreateTrainer && errorCreateTrainer) {
            this.setState({
                trainerError: true,
                loading: false
            })
        }
        if (prevProps.isCreateTrainerLoading && !isCreateTrainerLoading && createTrainer) {
            if (!errorCreateTrainer) {
                this.setState({
                    loading: false,
                    trainerError: false,
                });
            }
        }
        if (prevProps.isTrainerListLoading && !isTrainerListLoading && trainersList) {
            if (!errorTrainerList) {
                this.props.fetchTrainersList(0,50);
                departmentToString(departmentList.items,trainersList.items);
                this.setState({
                    trainer: trainersList.items,
                });
            }
        }

    }

    componentDidMount() {
        const {
            fetchTrainersList,
            fetchDepartmentList
        } = this.props;
        fetchDepartmentList(0, 50);
        fetchTrainersList(0, 50);
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.trainerModel[name] = value;
        this.setState({
            departmentListError: false,
            trainerError: false,
        })
    }

    handleClick = () => {
        const {
            createTrainer,
        } = this.props;
        createTrainer(this.trainerModel);
        console.log(this.trainerModel);
    }

    render() {
        const {
            departments,
            loading,
            trainerError,
            trainer,
            trainerColumns
        } = this.state;
        const { classes } = this.props;

        return (
            <div className={classes.root}>
                <Grid container spacing={24}>
                    <Typography
                        pageHeader={translate('Trainers')}
                    />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12}>
                        <Input
                            error={trainerError}
                            onChange={this.handleChange}
                            labelName={translate('Name')}
                            inputType="text"
                            name="name"
                            value={this.trainerModel.name}
                        />
                        <Input
                            error={trainerError}
                            onChange={this.handleChange}
                            labelName={translate('Surname')}
                            inputType="text"
                            name="surname"
                            value={this.trainerModel.surname}
                        />
                        <Input
                            error={trainerError}
                            onChange={this.handleChange}
                            labelName={translate('EmailAddress')}
                            inputType="text"
                            name="email"
                            value={this.trainerModel.email}
                        />
                        <Input
                            error={trainerError}
                            onChange={this.handleChange}
                            labelName={translate('PhoneNumber')}
                            inputType="text"
                            name="phoneNumber"
                            value={this.trainerModel.phoneNumber}
                        />
                        <DropDown
                            error={trainerError}
                            onChange={this.handleChange}
                            data={departments}
                            labelName={translate('Department')}
                            name="departmentId"
                            value={this.trainerModel.departmentId}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Divider />
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={10}>
                        <Input
                            error={trainerError}
                            onChange={this.handleChange}
                            labelName={translate('TrainerExpertise')}
                            inputType="text"
                            name="professionId"
                            value={this.trainerModel.professionId}
                        />
                    </Grid>
                    <Grid item xs={12} sm={2}>
                        <Button
                            buttonColor="secondary"
                            buttonName={translate('Add')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Chip
                            data={this.state.trainerExpertiseData}>
                        </Chip>
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12}>
                        <Divider />
                    </Grid>
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            disabled={loading}
                            onClick={this.handleClick}
                        />
                        {loading && (
                            <CircularProgress
                                size={24}
                                disableShrink={true}
                                className={classes.buttonProgress}
                            />
                        )}
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Table
                            columns={trainerColumns}
                            data={trainer}
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }
}

const mapStateToProps = state => {
    const {
        isCreateTrainerLoading,
        createTrainer,
        errorCreateTrainer
    } = state.createTrainer;

    const {
        errorDepartmentList,
        isDepartmentListLoading,
        departmentList
    } = state.departmentList;

    const {
        isTrainerListLoading,
        errorTrainerList,
        trainersList
    } = state.trainersList;

    return {
        isCreateTrainerLoading,
        createTrainer,
        errorCreateTrainer,
        errorDepartmentList,
        isDepartmentListLoading,
        departmentList,
        isTrainerListLoading,
        errorTrainerList,
        trainersList
    };
};

const mapDispatchToProps = {
    createTrainer,
    fetchTrainersList,
    fetchDepartmentList
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(Trainers));