import React from 'react';
import { ADD, TRAINEE, EXPORT_FROM_EXCEL, WORKING_STATUS, 
         NAME, SURNAME, DEPARTMENT, NATIONAL_IDENTITY_NUMBER, 
         PHONE_NUMBER, GENDER, TRAINEES } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Divider} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';

class Kisiler extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    calismaDurumu: [
        { key: 1, label: 'Çalışan',  },
        { key: 2, label: 'Aday',  },
        { key: 3, label: 'Stajyer',  },
    ],
    kisidepartmanData: [
        { key: 1, label: 'Tasarım', }
    ],
    cinsiyet: [
      { key: 1, label: 'Erkek', },
      { key: 2, label: 'Kadın', }
    ],
    kisilerColumns: ['Durum', 'Ad', 'Soyad', 'Departman', 'Cep Telefonu', 'Cinsiyet',' '],
    kisilerData: [{  'id': 0,
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
          <a href="#kisiler" className={classes.kisilerLink}>
           {`${TRAINEE} ${ADD}`}
          </a>
          </Grid>
          <Grid item xs={6} sm={3}>
            <Button 
              buttonColor="primary"                       
              buttonIcon="file-excel"                     
              buttonName= {EXPORT_FROM_EXCEL}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
          
            <DropDown 
              data={this.state.calismaDurumu}                        
              labelName={WORKING_STATUS}                   
              describable
            />
            <Input 
              labelName={NAME}                                    
              inputType="text"
            />
            <Input 
              labelName={SURNAME}                                     
              inputType="text"
            />
            <DropDown 
              data={this.state.kisidepartmanData}                       
              labelName={DEPARTMENT}   
            />
            <Input 
              labelName={NATIONAL_IDENTITY_NUMBER}                                   
              inputType="text"
            />
            <Input 
              labelName={PHONE_NUMBER}                                        
              inputType="text"
            />
            <DropDown 
              data={this.state.cinsiyet}                     
              labelName={GENDER} 
            />
          
          </Grid>
          <Grid item xs={12} sm={12}><Divider/></Grid>
          <Grid item xs={12} sm={12}>
          <a href='#kisiEkle' className={classes.kisilerLink}>
            {TRAINEES}
          </a>
          </Grid>
          <Grid item xs={12} sm={12}>  
            <div id="kisiler">
              <Table 
                columns={this.state.kisilerColumns}                    
                data={this.state.kisilerData}
              />
            </div>
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Kisiler);