import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
    Grid,
    Paper,
    Divider
}
    from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';
import Chip from '../../components/Chip/ChipComponent';

class Trainers extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trainersDepartmentData: [
                { id: 1, name: 'Tasarım', }
            ],
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
                { title: "Departman", field: "department" },
                { title: "Uzmanlık Alanları", field: "expertise"}
            ],
            trainerData: [{
                'id': 0,
                'name': 'Alper',
                'surname': 'Halıcı',
                'email': 'alper@arena.com',
                'phoneNumber': '0555 555 55 55',
                'department': 'Tasarım',
                'expertise': 'Web Tasarım, Ticaret Hukuku'
            },
            ]
        };
    }


    render() {
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
                            labelName={translate('Name')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('Surname')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('EmailAddress')}
                            inputType="text"
                        />
                        <Input
                            labelName={translate('PhoneNumber')}
                            inputType="text"
                        />
                        <DropDown
                            data={this.state.trainersDepartmentData}
                            labelName={translate('Department')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Divider />
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={10}>
                        <Input
                            labelName={translate('TrainerExpertise')}
                            inputType="text"
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
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Table
                            columns={this.state.trainerColumns}
                            data={this.state.trainerData}
                        />
                    </Grid>
                </Grid>
            </div>
        );
    }
}

export default withStyles(Style)(Trainers);