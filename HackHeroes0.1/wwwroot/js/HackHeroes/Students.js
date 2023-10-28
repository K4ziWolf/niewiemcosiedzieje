$(document).ready(function () {

    const RenderStudents = (students, container) => {
        container.empty();



        for (const student of students) {
            container.append(
                `<div class="card border-secondary mb-3 mx-1 student-card" style="max-width: 18rem;">
          <div class="card-header">Imiê: ${student.firstName}</div>
          <div class="card-body">
            <h5 class="card-title">Nazwisko: ${student.lastName}</h5> 
            <h5 class="card-title">Numer: ${student.number}</h5>            
            <button  type="button" class="btn btn-danger delete-student card-del-btn" data-student-key="${student.studentKey}" data-bs-toggle="modal" data-bs-target="#deleteStudentModal">
            Usuñ
            </button>
                     <a class="btn btn-primary card-btn" href="/HackHeroes/${student.studentKey}/EditStudent">Edytuj </a>
          </div>
        </div>`);

        }


        container.on('click', '.delete-student', function (event) {
            const studentKey = $(this).data('student-key');
            $('#deleteStudentModal').data('student-key', studentKey); // Przekazywanie studentKey do modalu
        });
    }




    const LoadStudents = () => {
        const container = $("#students");
        const classEncodedName = container.data("encodedName");

        $.ajax({
            url: `/HackHeroes/${classEncodedName}/Student`,
            type: 'get',
            success: function (data) {
                if (!data.length) {
                    container.html('<p class="your-classes-text">There are no students</p>');
                } else {
                    RenderStudents(data, container);
                }
            },
            error: function () {

            }
        });
    };

    LoadStudents()



    $("#deleteStudentModal form").submit(function (event) {
        event.preventDefault();
        const studentKey = $('#deleteStudentModal').data('student-key');

        $.ajax({
            url: `/HackHeroes/${studentKey}/Student`,
            type: $(this).attr('method'),
            success: function (data) {

                toastr["success"]("Deleted student")
                LoadStudents()

            },
            error: function () {

                toastr["error"]("Something went wrong")
            }
        })

    });

    $("#addStudentModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {

                toastr["success"]("Student added")
                LoadStudents()

            },
            error: function () {

                toastr["error"]("Student add error. Wrong student data")
            }
        })

    });
});