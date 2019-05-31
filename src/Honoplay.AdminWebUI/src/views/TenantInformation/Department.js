import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import SimpleTable from '../../components/Table/SimpleTable';

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

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12}/>
          <Grid item xs={12} sm={7}>
            <Input 
              labelName={translate('Department')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={5} sm={2}>
            <Button  
              buttonColor="secondary" 
              buttonName={translate('Add')}
            />
          </Grid>
          <Grid item xs={7} sm={3}>
            <Button 
              buttonColor="primary" 
              buttonIcon="file-excel" 
              buttonName={translate('ExportFromExcel')}
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

export default withStyles(Style)(Department);