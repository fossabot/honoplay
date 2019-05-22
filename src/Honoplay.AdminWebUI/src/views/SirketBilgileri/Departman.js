import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Divider} from '@material-ui/core';
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
            <Input labelName="Departman" 
                   inputId="inputDepartman" 
                   inputType="text"
            />
          </Grid>
          <Grid item xs={5} sm={2}>
            <Button  buttonColor="secondary" 
                     buttonName="Ekle"/>
          </Grid>
          <Grid item xs={7} sm={3}>
            <Button buttonColor="primary" 
                    buttonIcon="file-excel" 
                    buttonName="Excel'den Aktar"/>     
          </Grid>
          <Grid item xs={12} sm={12}> 
            <SimpleTable data={this.state.sirketdepartmanData}/>
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Departman);