import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Divider} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';

class Trainee extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    workingStatus: [
        { id: 1, name: 'Çalışan',  },
        { id: 2, name: 'Aday',  },
        { id: 3, name: 'Stajyer',  },
    ],
    traineeDepartmentData: [
        { id: 1, name: 'Tasarım', }
    ],
    gender: [
      { id: 1, name: 'Erkek', },
      { id: 2, name: 'Kadın', }
    ],
    traineeColumns: ['Durum', 'Ad', 'Soyad', 'Departman', 'Cep Telefonu', 'Cinsiyet',' '],
    traineeData: [{  'id': 0,
                     'Durum':'Stajyer',
                     'Ad': 'Şaduman',
                     'Soyad': 'Küçük',
                     'Departman': 'Pazarlama',
                     'Cep Telefonu': '0555 555 55 55',
                     'Cinsiyet': 'Kadın'
                    },
                    {'id': 1,
                     'Durum':'Çalışan',
                     'Ad': 'Alper',
                     'Soyad': 'Halıcı',
                     'Departman': 'Pazarlama',
                     'Cep Telefonu': '0555 555 55 55',
                     'Cinsiyet': 'Erkek'
                    }
                ]
  };
}

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root} id="kisiEkle">
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12}/>
          <Grid item xs={6} sm={9}>
          <div />
          <a href="#kisiler" className={classes.linkStyle}>
           {`${translate('Trainee')} ${translate('Add')}`}
          </a>
          </Grid>
          <Grid item xs={6} sm={3}>
            <Button 
              buttonColor="primary"                       
              buttonIcon="file-excel"                     
              buttonName= {translate('ExportFromExcel')}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
          
            <DropDown 
              data={this.state.workingStatus}                        
              labelName={translate('WorkingStatus')}                   
              describable
            />
            <Input 
              labelName={translate('Name')}                                    
              inputType="text"
            />
            <Input 
              labelName={translate('Surname')}                                     
              inputType="text"
            />
            <DropDown 
              data={this.state.traineeDepartmentData}                       
              labelName={translate('Department')}   
            />
            <Input 
              labelName={translate('NationalIdentityNumber')}                                   
              inputType="text"
            />
            <Input 
              labelName={translate('PhoneNumber')}                                        
              inputType="text"
            />
            <DropDown 
              data={this.state.gender}                     
              labelName={translate('Gender')} 
            />
          
          </Grid>
          <Grid item xs={12} sm={12}><Divider/></Grid>
          <Grid item xs={12} sm={12}>
          <a href='#kisiEkle' className={classes.linkStyle}>
            {translate('Trainees')}
          </a>
          </Grid>
          <Grid item xs={12} sm={12}>  
            <div id="kisiler">
              <Table 
                columns={this.state.traineeColumns}                    
                data={this.state.traineeData}
              />
            </div>
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Trainee);