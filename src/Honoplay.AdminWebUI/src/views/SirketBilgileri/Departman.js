import React from 'react';
import terasuProxy from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import SimpleTable from '../../components/Table/SimpleTable';

class Departman extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    sirketdepartmanData: [
      { key: 1, label: 'Tasarım',  },
      { key: 2, label: 'Yazılım',  },
      { key: 3, label: 'Yönetim',  },
      { key: 4, label: 'Satış Pazarlama',  },
      { key: 5, label: 'İnsan Kaynakları',  },   
      { key: 6, label: 'Muhasebe',  },  
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
              labelName={terasuProxy.translate('Department')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={5} sm={2}>
            <Button  
              buttonColor="secondary" 
              buttonName={terasuProxy.translate('Add')}
            />
          </Grid>
          <Grid item xs={7} sm={3}>
            <Button 
              buttonColor="primary" 
              buttonIcon="file-excel" 
              buttonName={terasuProxy.translate('ExportFromExcel')}
            />     
          </Grid>
          <Grid item xs={12} sm={12}> 
            <SimpleTable 
              data={this.state.sirketdepartmanData}
              header={terasuProxy.translate('TenantDepartments')}
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Departman);