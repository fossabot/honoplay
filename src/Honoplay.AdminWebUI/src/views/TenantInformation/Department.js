import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import SimpleTable from '../../components/Table/SimpleTable';

import { connect } from "react-redux";
import { fetchDepartmentList, createDepartment } from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";
import { isError } from 'util';

class Department extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      departments: [],
      loading: false,
      departmentError: false,
      departmentListError: false,
    };
  }

  departmentModel = {
    departments: []
  };

  componentDidUpdate(prevProps) {
    const { departmentList,
      isDepartmentListLoading,
      errorDepartmentList,
      errorCreateDepartment,
      isCreateDepartmentLoading,
      departments } = this.props;
    if( !prevProps.errorDepartmentList && errorDepartmentList ) {
      this.setState({
        departmentListError: true
      })
    }
    if ( prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList ) {
      console.log('deparment:', departmentList.items);
      this.setState({
        departments: departmentList.items
      })
    }
    if( !prevProps.isCreateDepartmentLoading && isCreateDepartmentLoading ) {
      this.setState({
        loading: true
      })
    }
    if( !prevProps.errorCreateDepartment && errorCreateDepartment ) {
      console.log('error');
      this.setState({
        departmentError: true,
        loading: false
      })
    }
    if ( prevProps.isCreateDepartmentLoading && !isCreateDepartmentLoading && departments ) {
      console.log(departments.items[0].departments[0]);
      console.log('push');
      if( !errorCreateDepartment ) {
        console.log('burdaaaa', errorCreateDepartment);
        this.setState({
          deparments: this.state.departments.push(departments.items[0].departments[0]),
          loading: false,
          departmentError: false,
        })
        console.log(this.state.departments);
      }
    }
  }

  componentDidMount() {
    this.props.fetchDepartmentList(0, 50);
  }

  handleClick = () => {
    this.props.createDepartment(this.departmentModel);
  }

  render() {
    const { loading, departmentError } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={6} sm={11}>
            <Input
              error = {departmentError}
              onChange={value => {this.departmentModel.departments = [value];
                this.setState({departmentError: false})
              }}
              labelName={translate('Department')}
              inputType="text"
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
              data={this.state.departments}
              header={translate('TenantDepartments')}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const { errorDepartmentList, isDepartmentListLoading, departmentList } = state.departmentList;
  const { errorCreateDepartment, isCreateDepartmentLoading, departments } = state.createDepartment;
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