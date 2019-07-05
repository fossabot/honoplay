import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Grid,
  CircularProgress
} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import SimpleTable from '../../components/Table/SimpleTable';

import { connect } from "react-redux";
import {
  fetchDepartmentList,
  createDepartment
} from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";

class Department extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      department: [],
      loading: false,
      departmentError: false,
      departmentListError: false,
    };
  }

  departmentModel = {
    departments: []
  };

  componentDidUpdate(prevProps) {
    const {
      departmentList,
      isDepartmentListLoading,
      errorDepartmentList,
      errorCreateDepartment,
      isCreateDepartmentLoading,
      departments
    } = this.props;

    const { department } = this.state;

    if (!prevProps.errorDepartmentList && errorDepartmentList) {
      this.setState({
        departmentListError: true
      })
    }
    if (prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList) {
      this.setState({
        department: departmentList.items
      })
    }
    if (!prevProps.isCreateDepartmentLoading && isCreateDepartmentLoading) {
      this.setState({
        loading: true
      })
    }
    if (!prevProps.errorCreateDepartment && errorCreateDepartment) {
      this.setState({
        departmentError: true,
        loading: false
      })
    }
    if (prevProps.isCreateDepartmentLoading && !isCreateDepartmentLoading && departments) {
      if (!errorCreateDepartment) {
        this.setState({
          deparment: department.push(departments.items[0].departments[0]),
          loading: false,
          departmentError: false,
        });
      }
    }

  }

  componentDidMount() {
    const { fetchDepartmentList } = this.props;
    fetchDepartmentList(0, 100);
  }

  handleClick = () => {
    const { createDepartment } = this.props;
    createDepartment(this.departmentModel);
  }

  handleChange = (e) => {
    const { name, value } = e.target;
    this.departmentModel[name] = [value];
    this.setState({ departmentError: false })
  }

  render() {
    const { loading, departmentError, department } = this.state;
    const { classes } = this.props;
    return (
      <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={6} sm={11}>
            <Input
              error={departmentError}
              onChange={this.handleChange}
              labelName={translate('Department')}
              inputType="text"
              name="departments"
              value={this.departmentModel.departments}
            />
          </Grid>
          <Grid item xs={6} sm={1}>
            <Button
              buttonColor="secondary"
              buttonName={translate('Add')}
              onClick={this.handleClick}
              disabled={loading}
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
            <SimpleTable
              data={department}
              header={translate('TenantDepartments')}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    errorDepartmentList,
    isDepartmentListLoading
  } = state.departmentList;
  const {
    errorCreateDepartment,
    isCreateDepartmentLoading,
    departments
  } = state.createDepartment;
  const departmentList = state.departmentList.departmentList;

  return {
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList,
    errorCreateDepartment,
    isCreateDepartmentLoading,
    departments
  };
};

const mapDispatchToProps = {
  fetchDepartmentList,
  createDepartment
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Department));