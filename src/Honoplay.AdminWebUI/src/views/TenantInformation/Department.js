import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import SimpleTable from '../../components/Table/SimpleTable';

import { connect } from "react-redux";
import { fetchDepartmentList, createDepartment} from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";

class Department extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    tanantDepartmentData: [
      { id: 1, name: 'Tasarım',  },
      { id: 2, name: 'Yazılım',  },
      { id: 3, name: 'Yönetim',  },
      { id: 4, name: 'Satış Pazarlama',  },
      { id: 5, name: 'İnsan Kaynakları',  },   
      { id: 6, name: 'Muhasebe',  },  
    ]
  };
}

departmentModel = {
  departments: []
};

componentDidMount() {
  this.props.fetchDepartmentList(1,10);
}

handleClick = () => {
  this.props.createDepartment(this.departmentModel);
  console.log(this.departmentModel);
}

render() {
  const { classes } = this.props;
    return (
        <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12}/>
          <Grid item xs={6} sm={11}>
            <Input 
              onChange = { value => this.departmentModel.departments = [value] }
              labelName={translate('Department')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={6} sm={1}>
            <Button  
              buttonColor="secondary" 
              buttonName={translate('Add')}
              onClick={this.handleClick}
            />
          </Grid>
          <Grid item xs={12} sm={12}> 
            <SimpleTable 
              data={this.state.tanantDepartmentData}
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
  return { errorDepartmentList, 
          isDepartmentListLoading, 
          departmentList, 
          errorCreateDepartment, 
          isCreateDepartmentLoading, 
          departments };
};

const mapDispatchToProps = {
  fetchDepartmentList,
  createDepartment
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Department));